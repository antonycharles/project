using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Application.Mappers;
using Accounts.Core.Handlers;
using Accounts.Core.Requests;
using Accounts.Core.Responses;
using Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Application.Handlers
{
    public class CompanyHandler : ICompanyHandler
    {
        private readonly AccountsContext _context;

        public CompanyHandler(AccountsContext context)
        {
            _context = context;
        }
        
        public async Task<CompanyResponse> CreateAsync(CompanyRequest request)
        {
            var company = request.ToCompany();

            _context.Companies.Add(company);

            await _context.SaveChangesAsync();

            return company.ToCompanyResponse();
        }

        public async Task<List<CompanyResponse>> GetByUserIdAsync(Guid userId)
        {
            var resultado =  await _context.UserProfiles
                .Where(up => up.UserId == userId && up.IsDeleted == false && up.Company.IsDeleted == false && up.Company.Status == Core.Enums.StatusEnum.Active)
                .Select(up => up.Company)
                .Distinct()
                .ToListAsync();

            return resultado.Select(c => c.ToCompanyResponse()).ToList();
        }
    }
}