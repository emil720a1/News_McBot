using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class SentNewsConfiguration : IEntityTypeConfiguration<SentNews>
{
    public void Configure(EntityTypeBuilder<SentNews> builder)
    {
        builder.ToTable("SentNews");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("sent_news_id");
        
        builder.Property(x => x.ArticleUrlHash)
            .HasColumnName("article_url_hash")
            .HasMaxLength(64)
            .IsRequired();

        builder.HasIndex(x => new { x.UserId, x.ArticleUrlHash })
            .IsUnique();
        
        builder.Property(x => x.SentAt)
            .HasColumnName("sent_at")
            .IsRequired();
        
        builder.HasOne(x => x.User)
            .WithMany(x => x.SentNews)
            .HasForeignKey(x => x.UserId);
    }
}