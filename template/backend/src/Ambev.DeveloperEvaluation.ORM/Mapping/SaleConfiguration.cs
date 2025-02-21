using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(u => u.SaleNumber)
            .IsRequired()
            .HasColumnType("integer") 
            .UseIdentityColumn(); 


        builder.Property(u => u.SaleDate)
            .IsRequired()
            .HasColumnType("timestamptz");

        builder.Property(u => u.Customer)
            .IsRequired()
            .HasColumnType("varchar(255)");

        builder.Property(u => u.TotalAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(u => u.Branch)
            .IsRequired()
            .HasColumnType("varchar(255)");

        builder.Property(u => u.IsCancelled)
            .IsRequired()
            .HasColumnType("boolean")
            .HasDefaultValue(false);

        builder.HasMany(s => s.Items)
            .WithOne()
            .HasForeignKey(si => si.SaleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
