using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Entities;
using Accounts.Core.Requests;
using Accounts.Core.Responses;

namespace Accounts.Application.Mappers
{
    public static class CompanyMap
    {
        public static Company ToCompany(this CompanyRequest request)
        {
            return new Company
            {
                Name = request.Name.Trim(),
                Status = Core.Enums.StatusEnum.Active,
            };
        }

        public static CompanyResponse ToCompanyResponse(this Company company)
        {
            return new CompanyResponse
            {
                Id = company.Id,
                Name = company.Name,
                CreatedAt = company.CreatedAt,
                UpdatedAt = company.UpdatedAt,
                Status = company.Status,
            };
        }
    }
}