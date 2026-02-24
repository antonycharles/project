using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Entities;
using Accounts.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounts.Infrastructure.EntitiesConfigurations
{
    public class AppCallbackEntityConfiguration : IEntityTypeConfiguration<AppCallback>
    {
        public void Configure(EntityTypeBuilder<AppCallback> builder)
        {
            builder
                .Property(s => s.Status)
                .HasDefaultValue(StatusEnum.Active);
        }
    }
}