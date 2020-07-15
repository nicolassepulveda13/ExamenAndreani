using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Api_Geo.Models
{
    public partial class Prueba_AndreaniContext : DbContext
    {
        public Prueba_AndreaniContext()
        {
        }

        public Prueba_AndreaniContext(DbContextOptions<Prueba_AndreaniContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Pedido> Pedido { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=sqldata;Database=Prueba_Andreani;User=SA;Password=Pass@word;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.Property(e => e.Calle)
                    .IsRequired()
                    .HasColumnName("calle")
                    .HasViewColumnName("calle")
                    .HasMaxLength(50);

                entity.Property(e => e.Ciudad)
                    .IsRequired()
                    .HasColumnName("ciudad")
                    .HasViewColumnName("ciudad")
                    .HasMaxLength(50);

                entity.Property(e => e.Numero)
                    .IsRequired()
                    .HasColumnName("numero")
                    .HasViewColumnName("numero")
                    .HasMaxLength(50);

                entity.Property(e => e.Pais)
                    .IsRequired()
                    .HasColumnName("pais")
                    .HasViewColumnName("pais")
                    .HasMaxLength(50);

                entity.Property(e => e.Provincia)
                    .IsRequired()
                    .HasColumnName("provincia")
                    .HasViewColumnName("provincia")
                    .HasMaxLength(50);

                entity.Property(e => e.lat)
                .HasColumnName("latitud")
                .HasViewColumnName("latitud")
                .HasMaxLength(100);

                entity.Property(e => e.lon)
                .HasColumnName("longitud")
                .HasViewColumnName("longitud")
                .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
