using DataAccess.Models.Movies.Actors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class ActorConfiguration : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.ToTable("Actors");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Firstname)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(a => a.Lastname)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasMany(a => a.MovieActors)
                   .WithOne(ma => ma.Actor)
                   .HasForeignKey(ma => ma.ActorId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
