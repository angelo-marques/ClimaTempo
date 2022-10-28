using Dominio.DTOs;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infraestrutura.Interface
{
    public interface IPrevisaoClimaRepositorio : IDisposable
    {
        List<PrevisaoClima> ObterListaDePrevisaoClima();
        Task<PrevisaoClima> ObterPrevisaoClimaPorId(int previsaoTempoId);
        PrevisaoClima ObterPrevisarClimaPorUfCidade(string estado, string cidade);
        Task<PrevisaoClimaDTO> InsertPrevisaoClima(PrevisaoClimaDTO previsaoClima);
        void UpdatePrevisaoClima(PrevisaoClima previsaoClima);
        Task DeletePrevisaoClima(int previsaoClimaId);
        Task<int> SalvarDados();
    }
}
