using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Core.Entities;
using Family.Accounts.Core.Exceptions;
using Family.Accounts.Core.Handlers;
using Family.Accounts.Core.Requests;
using Family.Accounts.Core.Responses;
using Family.Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Family.Accounts.Application.Handlers
{
    public class ProfileHandler : IProfileHandler
    {
        private readonly AccountsContext _context;

        public ProfileHandler(AccountsContext context){
            _context = context;
        }
    }
}