using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MapeamentoDados
{
    public class CidadeMapeamento : IEntityTypeConfiguration<Cidade>
    {
        public void Configure(EntityTypeBuilder<Cidade> builder)
        {
            builder.ToTable("Cidade");
            builder.HasKey(c => c.Id).HasName("Id");
            builder.Property(c => c.Nome).HasColumnName("Nome").HasColumnType("varchar(200)");
            builder.Property(c => c.EstadoId).HasColumnName("EstadoId");
            builder.HasOne(c => c.Estado).WithMany().HasForeignKey(p => p.EstadoId);
        }
    }
}
