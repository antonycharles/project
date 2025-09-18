using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Application.Mappers;
using Team.Accounts.Application.Providers;
using Team.Accounts.Core.Entities;
using Team.Accounts.Core.Enums;
using Team.Accounts.Core.Exceptions;
using Team.Accounts.Core.Handlers;
using Team.Accounts.Core.Requests;
using Team.Accounts.Core.Responses;
using Team.Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Team.Accounts.Application.Handlers
{
    public class ClientHandler : IClientHandler
    {

        private readonly AccountsContext _context;
        private readonly IPasswordProvider _passwordProvider;

        public ClientHandler(
            AccountsContext context,
            IPasswordProvider passwordProvider){
            _context = context;
            _passwordProvider = passwordProvider;
        }

        public async Task<ClientResponse> CreateAsync(ClientRequest request)
        {
            var client = request.ToClient();
            client.Password = _passwordProvider.HashPassword(request.Password); 

            await ValidExistsAsync(client);

            _context.Add(client);

            await _context.SaveChangesAsync();

            return client.ToClientResponse();
        }

        public async Task DeleteAsync(Guid id)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(w => w.Id == id && w.IsDeleted == false);

            if(client == null)
                throw new NotFoundException("Client not found");

            client.IsDeleted = true;

            _context.Update(client);

            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedResponse<ClientResponse>> GetAsync(PaginatedRequest request)
        {
            var query = _context.Clients.AsNoTracking()
                .Where(w => w.IsDeleted == false);

            if(request.Search is not null)
                query = query.Where(w => w.Name.ToLower() == request.Search.ToLower());

            var clients = await query
            .OrderBy(o => o.Name)
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();
            
            var totalItems = await query.CountAsync();
            var response = clients.Select(s => s.ToClientResponse()).ToList();
            return new PaginatedResponse<ClientResponse>(response, totalItems, request.PageIndex, request.PageSize, request);
        }

        public async Task<ClientResponse> GetByIdAsync(Guid id)
        {
            var client = await _context.Clients.AsNoTracking()
                .Include(i => i.ClientProfiles)
                .ThenInclude(i => i.Profile)
                .FirstOrDefaultAsync(w => w.Id == id && w.IsDeleted == false);

            if(client == null)
                throw new NotFoundException("Client not found");

            return client.ToClientResponse();
        }

        public async Task UpdateAsync(Guid id, ClientUpdateRequest request)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(w => w.Id == id && w.IsDeleted == false);

            if(client == null)
                throw new NotFoundException("Client not found");
            
            client.Update(request);

            if(request.Password != null && request.Password != "")
                client.Password = _passwordProvider.HashPassword(request.Password);

            await ValidExistsAsync(client);

            _context.Update(client);

            await _context.SaveChangesAsync();
        }

        private async Task ValidExistsAsync(Client client)
        {
            var exist = await _context.Clients.AsNoTracking()
                .AnyAsync(w => w.Name == client.Name && w.Id != client.Id && w.IsDeleted == false);

            if(exist)
                throw new BusinessException("Client name already exists");
        }
    }
}