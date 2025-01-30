using DataAccess.Models.Sessions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations.Sessions;

public class SeatConfiguration : IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        builder.ToTable("Seats");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(s => s.RowNumber)
            .HasColumnName("row_number")
            .IsRequired();

        builder.Property(s => s.SeatNumber)
            .HasColumnName("seat_number")
            .IsRequired();

        builder.Property(s => s.IsBooked)
            .HasColumnName("is_booked")
            .IsRequired();

        builder.HasOne(s => s.Hall)
            .WithMany(h => h.Seats)
            .HasForeignKey(s => s.HallId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
