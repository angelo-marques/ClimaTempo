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

        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<PrevisaoClima> PrevisaoClima { get; set; }
    }
}
