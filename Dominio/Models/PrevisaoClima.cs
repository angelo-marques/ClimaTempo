using System;

namespace Dominio.Models
{
    public class PrevisaoClima
    {
        public PrevisaoClima() { }

        public PrevisaoClima(int id, int cidadeId, DateTime dataPrevisao, string clima, decimal temperaturaMinima, decimal temperaturaMaxima, Cidade cidade)
        {
            Id = id;
            CidadeId = cidadeId;
            DataPrevisao = dataPrevisao;
            Clima = clima;
            TemperaturaMinima = temperaturaMinima;
            TemperaturaMaxima = temperaturaMaxima;
            Cidade = cidade;
        }

        public int Id { get; set; }
        public int CidadeId { get; set; }
        public DateTime DataPrevisao { get; set; }
        public string Clima { get; set; }
        public decimal TemperaturaMinima { get; set; }
        public decimal TemperaturaMaxima { get; set; }
        public Cidade Cidade { get; set; }
    }
}
