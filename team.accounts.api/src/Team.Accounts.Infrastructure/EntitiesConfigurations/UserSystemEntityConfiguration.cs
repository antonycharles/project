using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Team.Accounts.Infrastructure.EntitiesConfigurations
{
    public class UserSystemEntityConfiguration : IEntityTypeConfiguration<UserSystem>
    {
        public void Configure(EntityTypeBuilder<UserSystem> builder)
        {
            builder
                .HasIndex(i => i.Name)
                .IsUnique();
        }
    }
}