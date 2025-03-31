using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RapidPay.Domain.Models;

namespace RapidPay.Infraestructure.Configurations
{
    public class FeeConfiguration : IEntityTypeConfiguration<FeeModel>
    {
        public void Configure(EntityTypeBuilder<FeeModel> builder)
        {
            builder.HasKey(f => f.Id);

            builder
                .Property(f => f.Id)
                .HasColumnName("id");

            builder
                .Property(f => f.Value)
                .HasColumnName("value");

            builder
                .Property(f => f.CreatedAt)
                .HasColumnName("created_at");

            builder
                .Property(f => f.UpdatedAt)
                .HasColumnName("updated_at");

            builder.ToTable("fees", schema: "rapidpay");
        }
    }
}
