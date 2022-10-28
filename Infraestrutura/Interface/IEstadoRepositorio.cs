using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infraestrutura.Interface
{
    public interface IEstadoRepositorio : IDisposable
    {
        Estado GetSiglaEstado(string nome);
        List<Estado> GetListaDeEstados();
        Task<Estado> GetEstadoPorId(int estadoId);
        Task<Estado> InsertEstado(Estado estado);
        void UpdateEstado(Estado estado);
        Task DeleteEstado(int estadoId);
        Task<int> SalvarDados();
    }
}
