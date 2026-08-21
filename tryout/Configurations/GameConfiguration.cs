using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tryout.Models;

namespace tryout.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(g => g.Description)
                .HasMaxLength(1000);

            builder.Property(g => g.Rating)
                .HasPrecision(3, 1);

            builder.Property(g => g.CreatedAt)
                .IsRequired();

            builder.HasOne(g => g.Genre)
                .WithMany(g => g.Games)
                .HasForeignKey(g => g.GenreId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
