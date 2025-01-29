using DataAccess.Models.Sessions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations.Sessions;

public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.ToTable("Sessions");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(s => s.StartTime)
            .HasColumnName("start_time")
            .IsRequired();

        builder.Property(s => s.Price)
            .HasColumnName("price")
            .IsRequired()
            .HasPrecision(10, 2);

        builder.HasOne(s => s.Movie)
            .WithMany(m => m.Sessions)
            .HasForeignKey(s => s.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.Hall)
            .WithMany(h => h.Sessions)
            .HasForeignKey(s => s.HallId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
