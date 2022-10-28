using Dominio.DTOs;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infraestrutura.Interface
{
    public interface ICidadeRepositorio : IDisposable
    {
        List<Cidade> ObterListaDeCidadesComEstados();
        Task<Cidade> ObterCidadePorId(int cidadeId);
        Cidade ObterCidade(string estado, string cidade);
        Task<CidadeDTO> InsertCidade(CidadeDTO cidadeDTO);
        void UpdateCidade(Cidade cidade);
        Task DeleteCidade(int cidadeId);
        Task<int> SalvarDados();
    }
}
