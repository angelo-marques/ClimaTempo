using Dominio.Models;
using MapeamentoDados;
using Microsoft.EntityFrameworkCore;

namespace DadosConexao
{
    public class ConexaoContexto : DbContext
    {
        public ConexaoContexto(DbContextOptions<ConexaoContexto> conexao) : base(conexao) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CidadeMapeamento());
            modelBuilder.ApplyConfiguration(new EstadoMapeamento());
            modelBuilder.ApplyConfiguration(new PrevisaoClimaMapeamento());
        }

        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<PrevisaoClima> PrevisaoClimas { get; set; }
    }
}
