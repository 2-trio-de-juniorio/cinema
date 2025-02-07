using DataAccess.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configurations
{
    public class UserPreferenceConfiguration : IEntityTypeConfiguration<UserPreference>
    {
        public void Configure(EntityTypeBuilder<UserPreference> builder)
        {
            builder.HasKey(up => up.Id);

            builder.HasOne(up => up.User)
                .WithMany(u => u.Preferences)
                .HasForeignKey(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(up => up.Movie)
                .WithMany()
                .HasForeignKey(up => up.MovieId)
                .OnDelete(DeleteBehavior.Cascade); 

            builder.HasIndex(up => new { up.UserId, up.MovieId }).IsUnique();
        }
    }
}
