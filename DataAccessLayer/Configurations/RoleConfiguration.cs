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
                    Id = "bb815bab-2485-47b1-b460-0639a8ba967b",
                    Name = UserRole.Admin,
                    NormalizedName = UserRole.Admin.ToUpper()
                },

                new()
                {
                    Id = "26eeb29f-7bc2-4fae-a926-a59fc11c5dfe",
                    Name = UserRole.User,
                    NormalizedName = UserRole.User.ToUpper()
                }
            ];
            
            builder.HasData(roles);
        }
    }
}