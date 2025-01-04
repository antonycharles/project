using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Family.Accounts.Core.Entities;
using Family.Accounts.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Family.Accounts.Infrastructure.EntitiesConfigurations
{
    public class ApplicationEntityConfiguration : IEntityTypeConfiguration<App>
    {
        public void Configure(EntityTypeBuilder<App> builder)
        {
            builder
                .Property(s => s.Status)
                .HasDefaultValue(StatusEnum.Active);
        }
    }
}