using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscriptions>
{
    public void Configure(EntityTypeBuilder<Subscriptions> builder)
    {
         builder.ToTable("Subscriptions");
         
         builder.HasKey(x => x.Id);
         builder.Property(x => x.Id)
             .HasColumnName("subscription_id");
         
         builder.Property(x => x.UserId)
             .HasColumnName("user_id")
             .IsRequired();
         
         builder.Property(x => x.Topic)
             .HasColumnName("topic")
             .HasMaxLength(100)
             .IsRequired();
         
         builder.Property(x => x.IsActive)
             .HasColumnName("is_active")
             .HasDefaultValue(true);
         
         builder.HasOne(x => x.User)
             .WithMany(x => x.Subscriptions)
             .HasForeignKey(x => x.UserId)
             .OnDelete(DeleteBehavior.Cascade);
    }
}