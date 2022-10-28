using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapeamentoDados
{
    public class PrevisaoClimaMapeamento : IEntityTypeConfiguration<PrevisaoClima>
    {
        public void Configure(EntityTypeBuilder<PrevisaoClima> builder)
        {
            builder.ToTable("PrevisaoClima");
            builder.HasKey(x => x.Id).HasName("Id");
            builder.Property(x => x.Clima).HasColumnName("Clima").HasColumnType("varchar(200)");
            builder.Property(x => x.TemperaturaMaxima).HasColumnName("TemperaturaMaxima");
            builder.Property(x => x.TemperaturaMinima).HasColumnName("TemperaturaMinima");
            builder.Property(x => x.DataPrevisao).HasColumnName("DataPrevisao");
            builder.Property(x => x.CidadeId).HasColumnName("CidadeId");

            builder.HasOne(c => c.Cidade).WithMany().HasForeignKey(p => p.CidadeId);
        }
    }
}
