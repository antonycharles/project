using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Application.Handlers;
using Family.Accounts.Application.Test.Fakes;
using Family.Accounts.Core.Enums;
using Family.Accounts.Core.Requests;
using Family.Accounts.Infrastructure.Data;
using Xunit;

namespace Family.Accounts.Application.Test.Handlers
{
    public class ApphandlerCreate
    {
        private readonly AccountsContext _accountsContext;
        private readonly AppHandler _appHandler;

        public ApphandlerCreate()
        {
            _accountsContext = DatabaseContextFake.Create();
            _appHandler = new AppHandler(_accountsContext);
        }

        [Theory]
        [InlineData("App name Xpto", 1, "app-name-xpto", StatusEnum.Active)]
        [InlineData("App name GDAER", 2, "app-name-gdaer", StatusEnum.Inactive)]
        public async Task Return_AppResponse_When_CreateAppIsSuccessfulAsync(string appName, int code, string slug, StatusEnum status)
        {
            //Arrange
            _accountsContext.Apps.AddRange(AppFake.Create().Generate(10));
            await _accountsContext.SaveChangesAsync();

            var appRequest = new AppRequest{
                Name = appName,
                Slug = slug,
                Code = code,
                Status = status
            };

            //Act
            var appResponse = await _appHandler.CreateAsync(appRequest);

            //Assert
            Assert.NotNull(appResponse);
            Assert.Equal(appRequest.Name, appResponse.Name);
            Assert.Equal(appRequest.Status, appResponse.Status);
        }
    }
}