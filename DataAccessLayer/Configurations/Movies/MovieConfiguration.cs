using DataAccess.Models.Movies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movies");


            builder.HasKey(m => m.Id);

            builder.Property(m => m.Title)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(m => m.Description)
                   .IsRequired();

            builder.Property(m => m.Duration)
                   .IsRequired();

            builder.Property(m => m.ReleaseDate)
                   .IsRequired();

            builder.Property(m => m.TrailerUrl)
                   .HasMaxLength(2083);

            builder.Property(m => m.PosterUrl)
                   .HasMaxLength(2083);

            builder.Property(m => m.Rating)
                   .HasPrecision(3, 1);

            builder.HasMany(m => m.MovieGenres)
                   .WithOne(mg => mg.Movie)
                   .HasForeignKey(mg => mg.MovieId);

            builder.HasMany(m => m.MovieActors)
                   .WithOne(ma => ma.Movie)
                   .HasForeignKey(ma => ma.MovieId);

            builder.HasMany(m => m.Sessions)
                   .WithOne(s => s.Movie)
                   .HasForeignKey(s => s.MovieId);
        }
    }
}
