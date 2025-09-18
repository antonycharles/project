using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team.Accounts.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Team.Accounts.Infrastructure.EntitiesConfigurations
{
    public class ProfilePermissionEntityConfiguration : IEntityTypeConfiguration<ProfilePermission>
    {
        public void Configure(EntityTypeBuilder<ProfilePermission> builder)
        {
            builder
                .HasKey(s => new { s.ProfileId, s.PermissionId });
        }
    }
}