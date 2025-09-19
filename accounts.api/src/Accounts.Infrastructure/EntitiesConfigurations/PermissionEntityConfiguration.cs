using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounts.Infrastructure.EntitiesConfigurations
{
    public class PermissionEntityConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder
                .Property(s => s.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasIndex(s => new { s.AppId, s.Role })
                .IsUnique();

        }
    }
}