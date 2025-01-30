using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            List<IdentityRole> roles =
            [
                new()
                {
                    Name = UserRole.Admin,
                    NormalizedName = UserRole.Admin.ToUpper()
                },

                new()
                {
                    Name = UserRole.User,
                    NormalizedName = UserRole.User.ToUpper()
                }
            ];
            
            builder.HasData(roles);
        }
    }
}