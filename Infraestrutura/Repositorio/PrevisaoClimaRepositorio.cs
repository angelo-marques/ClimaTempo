using DadosConexao;
using Dominio.DTOs;
using Dominio.Models;
using Infraestrutura.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorio
{
    public class PrevisaoClimaRepositorio : IPrevisaoClimaRepositorio, IDisposable
    {
        private readonly IEstadoRepositorio _estadoRepositorio;
        private ConexaoContexto _conexaoContexto;
        private bool disposed = false;
        public PrevisaoClimaRepositorio(ConexaoContexto conexaoContexto, IEstadoRepositorio estadoRepositorio)
        {
            this._conexaoContexto = conexaoContexto;
            this._estadoRepositorio = estadoRepositorio;
        }

        public async Task DeletePrevisaoClima(int previsaoClimaId)
        {
            PrevisaoClima previsaoClima = await _conexaoContexto.PrevisaoClima.FindAsync(previsaoClimaId);
            _conexaoContexto.PrevisaoClima.Remove(previsaoClima);
        }

        public List<PrevisaoClima> ObterListaDePrevisaoClima()
        {
            return _conexaoContexto.PrevisaoClima.Include(c => c.Cidade).ThenInclude(c => c.Estado).AsNoTracking().ToList(); ;
        }

        public PrevisaoClima ObterPrevisarClimaPorUfCidade(string uf, string cidade)
        {
            var retorno = _conexaoContexto.PrevisaoClima.Include(c => c.Cidade).ThenInclude(c => c.Estado)
                .Where(c => c.Cidade.Estado.UF.Equals(uf.ToUpper()) && c.Cidade.Nome.ToUpper().Equals(cidade.ToUpper()))
                .FirstOrDefault();
            return retorno;
        }

        public async Task<PrevisaoClima> ObterPrevisaoClimaPorId(int PrevisaoClimaId)
        {
            return await _conexaoContexto.PrevisaoClima.FindAsync(PrevisaoClimaId);
        }


        public virtual async Task<PrevisaoClimaDTO> InsertPrevisaoClima(PrevisaoClimaDTO previsaoClimaDTO)
        {
            // busar cidade

            PrevisaoClima previsaoClima = new ()
            {
                Id = previsaoClimaDTO.Id,
                DataPrevisao = previsaoClimaDTO.DataPrevisao,
                TemperaturaMinima = previsaoClimaDTO.TemperaturaMinima,
                TemperaturaMaxima = previsaoClimaDTO.TemperaturaMaxima,
                CidadeId = 0
               
            };

            await _conexaoContexto.PrevisaoClima.AddAsync(previsaoClima);
            await SalvarDados();

            return null;

          
        }

        public void UpdatePrevisaoClima(PrevisaoClima previsaoClima)
        {
            _conexaoContexto.Entry(previsaoClima).State = EntityState.Modified;
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
