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
    public class CidadeRepositorio : ICidadeRepositorio, IDisposable
    {
        private readonly IEstadoRepositorio _estadoRepositorio;
        private ConexaoContexto _conexaoContexto;
        private bool disposed = false;
        public CidadeRepositorio(ConexaoContexto conexaoContexto, IEstadoRepositorio estadoRepositorio)
        {
            this._conexaoContexto = conexaoContexto;
            this._estadoRepositorio = estadoRepositorio;
        }

        public async Task DeleteCidade(int cidadeId)
        {
            Cidade cidade = await _conexaoContexto.Cidades.FindAsync(cidadeId);
            _conexaoContexto.Cidades.Remove(cidade);
        }

        public List<Cidade> ObterListaDeCidadesComEstados()
        {
            return _conexaoContexto.Cidades.Include(c => c.Estado).AsNoTracking().ToList(); ;
        }

        public Cidade ObterCidade(string uf, string cidade)
        {
            var retorno = _conexaoContexto.Cidades.Include(c => c.Estado)
                .Where(c => c.Estado.UF.Equals(uf.ToUpper()) && c.Nome.ToUpper().Equals(cidade.ToUpper()))
                .FirstOrDefault();
            return retorno;
        }

        public async Task<Cidade> ObterCidadePorId(int cidadeId)
        {
            return await _conexaoContexto.Cidades.FindAsync(cidadeId);
        }


        public virtual async Task<CidadeDTO> InsertCidade(CidadeDTO cidadeDTO)
        {

            if (ObterCidade(cidadeDTO.Uf, cidadeDTO.Cidade) != null)
            {
                return null;
            }

            var estado = _estadoRepositorio.GetSiglaEstado(cidadeDTO.Uf);

            if (estado == null)
            {
                throw new ArgumentException("Erro nos dados");
            }

            Cidade cidade = new()
            {
                Nome = cidadeDTO.Cidade,
                EstadoId = estado.Id,
            };

            if (cidade != null)
            {
                await _conexaoContexto.Cidades.AddAsync(cidade);
                await SalvarDados();
               
                return cidadeDTO;
            }
            return null;
        }

        public void UpdateCidade(Cidade cidade)
        {
            _conexaoContexto.Entry(cidade).State = EntityState.Modified;
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
