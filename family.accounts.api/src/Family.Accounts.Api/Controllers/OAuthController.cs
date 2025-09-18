using Family.Accounts.Api.Helpers;
using Family.Accounts.Api.Responses;
using Family.Accounts.Core.Enums;
using Family.Accounts.Core.Exceptions;
using Family.Accounts.Core.Handlers;
using Family.Accounts.Core.Requests;
using Family.Accounts.Core.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

[ApiController]
[Route("v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class OAuthController : ControllerBase
{
    private readonly IClientAuthorizationHandler _clientAuthorizationHandler;
    private readonly IUserAuthorizationHandler _userAuthenticationHandler;
    private readonly IUserHandler _userHandler;
    private readonly IClientHandler _clientHandler;
    private readonly IDistributedCache _distributedCache;
    private readonly ITokenHandler _tokenHandler;

    public OAuthController(
        IClientAuthorizationHandler clientAuthorizationHandler,
        IClientHandler clientHandler,
        IUserAuthorizationHandler userAuthenticationHandler,
        IUserHandler userHandler,
        IDistributedCache distributedCache,
        ITokenHandler tokenHandler)
    {
        _clientAuthorizationHandler = clientAuthorizationHandler;
        _clientHandler = clientHandler;
        _userAuthenticationHandler = userAuthenticationHandler;
        _userHandler = userHandler;
        _distributedCache = distributedCache;
        _tokenHandler = tokenHandler;
    }

    private static Dictionary<string, string> authCodes = new();
    private static Dictionary<string, string> refreshTokens = new();

    [HttpPost("token")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> TokenAsync([FromForm] TokenRequest request)
    {
        try
        {
            if (request.GrantType == "client_credentials")
            {
                return await ClientCredentials(request);
            }

            if (request.GrantType == "authorization_code")
            {
                return await AuthorizationCode(request);
            }

            if (request.GrantType == "refresh_token")
            {
                return await RefreshTokenAsync(request);
            }
        }
        catch (BusinessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }


        return BadRequest("grant_type inválido");
    }

    private async Task<IActionResult> RefreshTokenAsync(TokenRequest request)
    {
        var jsonFromCache = await _distributedCache.GetStringAsync(request.Code);
        
        if (string.IsNullOrEmpty(jsonFromCache))
            return BadRequest("Expired or invalid refresh token");

        var result = JsonSerializer.Deserialize<AuthenticationResponse>(jsonFromCache);

        if(result.UserType == UserTypeEnum.client )
        {
            var client = await _tokenHandler.GenerateClientTokenAsync(result.AuthId, request.AppSlug ?? result.AppSlug);
            if (client == null)
                return BadRequest("Client not found");
        }
        else if(result.UserType == UserTypeEnum.user)
        {
            var user = await _tokenHandler.GenerateUserTokenAsync(result.AuthId, request.AppSlug ?? result.AppSlug);
            if (user == null)
                return BadRequest("User not found");
        }

        return BadRequest("User type not supported");
    }

    private async Task<IActionResult> AuthorizationCode(TokenRequest request)
    {
        var jsonFromCache = await _distributedCache.GetStringAsync(request.Code);
        if (string.IsNullOrEmpty(jsonFromCache))
            return BadRequest("Expired or invalid code");

        var result = JsonSerializer.Deserialize<AuthenticationResponse>(jsonFromCache);
        _distributedCache.Remove(request.Code);
        await AddCacheRefreshToken(result);

        return Ok(result);
    }

    private async Task AddCacheRefreshToken(AuthenticationResponse? result)
    {
        await _distributedCache.SetStringAsync(
            result.RefreshToken,
            JsonSerializer.Serialize(result),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(5)
            });
    }

    private async Task<IActionResult> ClientCredentials(TokenRequest request)
    {
        var client = await _clientAuthorizationHandler.AuthenticationAsync(new ClientAuthenticationRequest
        {
            ClientId = request.ClientId.Value,
            ClientSecret = request.ClientSecret,
            AppSlug = request.AppSlug
        });

        await AddCacheRefreshToken(client);
        return Ok(client);
    }

    [HttpPost("authentication")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthorizationCodeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AuthenticationAsync([FromBody] UserAuthenticationRequest request)
    {
        try
        {
            var result = await _userAuthenticationHandler.AuthenticationAsync(request);

            var key = Guid.NewGuid().ToString();
            await _distributedCache.SetStringAsync(
                key,
                JsonSerializer.Serialize(result),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                });

            return Ok(new AuthorizationCodeResponse{
                Code = key
            });
        }
        catch(BusinessException ex)
        {
            return Problem(ex.Message, statusCode: StatusCodes.Status401Unauthorized);
        }
        catch(Exception ex)
        {
            return Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("userInfo")]
    public async Task<IActionResult> UserInfoAsync()
    {
        try
        {
            if (User.Identity?.IsAuthenticated == false)
                return Unauthorized("User not authenticated");


            if (User.GetUserType() == UserTypeEnum.client.ToString())
            {
                var client = await _clientHandler.GetByIdAsync(User.GetId());
                return Ok(client);
            }
            else if(User.GetUserType() == UserTypeEnum.user.ToString())
            {
                var user = await _userHandler.GetByIdAsync(User.GetId());
                return Ok(user);
            }
            
            
            return BadRequest("User type not supported");
        }
        catch (BusinessException ex)
        {
            return Problem(ex.Message, statusCode: StatusCodes.Status400BadRequest);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

/*
    builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://meu-oauth-server";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidAudience = "resource-api",
            ValidateIssuer = true,
            ValidIssuer = "https://meu-oauth-server",
            ValidateIssuerSigningKey = true
        };
    });*/
}
