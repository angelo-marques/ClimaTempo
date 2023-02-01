using DadosConexao;
using Dominio.Models;
using Infraestrutura.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorio
{
    public class EstadoRepositorio : IEstadoRepositorio, IDisposable
    {
        private ConexaoContexto _conexaoContexto;
        private bool disposed = false;
        public EstadoRepositorio(ConexaoContexto conexaoContexto)
        {
            this._conexaoContexto = conexaoContexto;
        }

        public async Task DeleteEstado(int estadoId)
        {
            Estado estado = await _conexaoContexto.Estado.FindAsync(estadoId);
            _conexaoContexto.Estado.Remove(estado);
        }

        public List<Estado> GetListaDeEstados()
        {   
            return _conexaoContexto.Estado.AsNoTracking().ToList();
        }

        public async Task<Estado> GetEstadoPorId(int estadoId)
        {
            return await _conexaoContexto.Estado.FindAsync(estadoId);
        }

        public Estado GetSiglaEstado(string nome)
        {
            return _conexaoContexto.Estado.Where( x => x.UF.ToUpper().Equals(nome.ToUpper())).FirstOrDefault();
        }

        public virtual async Task<Estado> InsertEstado(Estado estado)
        {
            var action = new Estado();
            action.AcionarDadosEstado(estado.Id, estado.Nome, estado.UF);
            
           

            if (estado != null)
            {
                var result = await _conexaoContexto.Estado.AddAsync(estado);
                await SalvarDados();
                return result.Entity;
            }
            return null;
        }

        public void UpdateEstado(Estado estado)
        {
            _conexaoContexto.Entry(estado).State = EntityState.Modified;
        }

        public async Task<int> SalvarDados()
        {
            var result = await _conexaoContexto.SaveChangesAsync();
            Dispose();
            return result;
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _conexaoContexto.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
