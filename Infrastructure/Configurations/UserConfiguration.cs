using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("user_id");
        
        builder.Property(x => x.Username)
            .HasColumnName("username")
            .IsRequired();
        
        builder.Property(x => x.TelegramId)
            .HasColumnName("telegram_id")
            .IsRequired();
        
        builder.Property(a => a.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();
        
        builder.HasMany(x => x.Subscriptions)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.SentNews)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);
    }
}