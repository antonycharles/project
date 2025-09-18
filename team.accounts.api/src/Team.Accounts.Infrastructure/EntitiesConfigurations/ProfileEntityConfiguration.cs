using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Core.Entities;
using Team.Accounts.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Team.Accounts.Infrastructure.EntitiesConfigurations
{
    public class ProfileEntityConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {       
            builder
                .Property(s => s.Status)
                .HasDefaultValue(StatusEnum.Active);

            builder
                .Property(s => s.IsDefault)
                .HasDefaultValue(false);

        }
    }
}