using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RapidPay.Domain.Models;

namespace RapidPay.Infraestructure.Configurations
{
    public class CardConfiguration : IEntityTypeConfiguration<CardModel>
    {
        public void Configure(EntityTypeBuilder<CardModel> builder)
        {
            builder
                .HasKey(t => t.Id);

            builder
                .Property(t => t.Id)
                .HasColumnName("id");

            builder
                .Property(t => t.UserId)
                .HasColumnName("user_id");

            builder
                .Property(t => t.Number)
                .HasColumnName("number")
                .IsRequired()
                .HasMaxLength(256);

            builder
                .Property(t => t.CVV)
                .HasColumnName("cvv")
                .IsRequired()
                .HasMaxLength(64);

            builder
                .Property(t => t.ExpirationDate)
                .HasColumnName("expiration_date")
                .IsRequired();

            builder
                .Property(t => t.Balance)
                .HasColumnName("balance")
                .HasColumnType("decimal(18,2)");

            builder
                .Property(t => t.CreditLimit)
                .HasColumnName("credit_limit")
                .HasColumnType("decimal(18,2)");

            builder
                .Property(t => t.Active)
                .HasColumnName("active");

            builder
                .Property(t => t.CreatedAt)
                .HasColumnName("created_at");

            builder
                .Property(t => t.UpdatedAt)
                .HasColumnName("updated_at");

            builder
                .HasOne<UserModel>()
                .WithOne()  
                .HasForeignKey<CardModel>(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .ToTable("cards", schema: "rapidpay");
        }
    }
}
