using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItems");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(u => u.SaleId)
            .IsRequired()
            .HasColumnType("uuid");

        builder.Property(u => u.Product)
            .IsRequired()
            .HasColumnType("varchar(255)");

        builder.Property(u => u.Quantity)
            .IsRequired()
            .HasColumnType("int");

        builder.Property(u => u.UnitPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(u => u.Discount)
            .IsRequired()
            .HasColumnType("decimal(5,2)")
            .HasDefaultValue(0);

        builder.Property(u => u.TotalAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasOne<Sale>()
            .WithMany(s => s.Items)
            .HasForeignKey(u => u.SaleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
