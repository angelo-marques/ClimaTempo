using Dominio.DTOs;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infraestrutura.Interface
{
    public interface IPrevisaoTempoRepositorio : IDisposable
    {
        List<PrevisaoClima> ObterListaDePrevisaoTempo();
        Task<PrevisaoClima> ObterPrevisaoTempoPorId(int previsaoTempoId);
        PrevisaoClima ObterPrevisaoTempoPorCidade(string estado, string cidade);
        Task<PrevisaoClima> InsertPrevisaoTempo(PrevisaoClima previsaoClima);
        void UpdatePrevisaoTempo(PrevisaoClima previsaoClima);
        Task DeletePrevisaoTempo(int previsaoClimaId);
        Task<int> SalvarDados();
    }
}
