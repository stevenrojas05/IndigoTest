using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            // Configurar la clave primaria
            builder.HasKey(s => s.SaleId);

            // Configurar propiedades
            builder.Property(s => s.SaleDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(s => s.Total)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            // Configurar relación uno a muchos con SaleItems
            builder.HasMany(s => s.Items)
                .WithOne(si => si.Sale)
                .HasForeignKey(si => si.SaleId)
                .OnDelete(DeleteBehavior.Cascade);

            // Nombre de la tabla
            builder.ToTable("Sales", "arodriguez");
        }
    }
}

