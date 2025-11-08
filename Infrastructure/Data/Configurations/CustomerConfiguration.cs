using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            // Configurar la clave primaria
            builder.HasKey(c => c.CustomerId);

            // Configurar propiedades
            builder.Property(c => c.UserName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.PasswordHash)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            // Índice único para el Email
            builder.HasIndex(c => c.Email)
                .IsUnique()
                .HasDatabaseName("IX_Customer_Email");

            // Nombre de la tabla
            builder.ToTable("Customers", "arodriguez");
        }
    }
}

