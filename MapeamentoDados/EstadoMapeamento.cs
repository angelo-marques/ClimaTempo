using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MapeamentoDados
{
    public class EstadoMapeamento : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            builder.ToTable("Estado");
            builder.HasKey(x => x.Id).HasName("Id");
            builder.Property(x => x.Nome).HasColumnName("Nome").HasColumnType("varchar(200)");
            builder.Property(x => x.UF).HasColumnName("UF").HasColumnType("varchar(200)");
        }
    }
}
