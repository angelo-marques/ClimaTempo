using Azure.Messaging.ServiceBus;
using Dominio.DTOs;
using Dominio.Models;
using Infraestrutura.Interface;
using Infraestrutura.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClimaTempo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        private readonly ICidadeRepositorio _cidadeRepositorio;
        private readonly ServiceBusClient _serviceBusClient;

        public CidadeController(ICidadeRepositorio cidadeRepositorio, ServiceBusClient cliente)
        {
            this._cidadeRepositorio = cidadeRepositorio;
            this._serviceBusClient = cliente;
        }

        [HttpPost("CadastroCidadeViaFila")]
        public async Task<IActionResult> CadastroCidadeViaFila([FromBody] CidadeDTO cidadeDTO)
        {

            if(_cidadeRepositorio.ObterCidade(cidadeDTO.Uf, cidadeDTO.Cidade) != null) {
                return Ok("Já esta cadastrado na base de dados.");
            }

            string queueNome = "cidade";
            var enviar = _serviceBusClient.CreateSender(queueNome);
            string corpoMensagem = JsonSerializer.Serialize(cidadeDTO);
            var mensagem = new ServiceBusMessage(corpoMensagem);
            await enviar.SendMessageAsync(mensagem).ConfigureAwait(false);
            await enviar.CloseAsync();
                    
            return Ok("Cadastrado com sucesso!");
          
        }

        [HttpGet("BuscaListaDeCidadesComEstado")]
        public List<Cidade> GetBuscaListaDeCidadesComEstados()
        {
            return _cidadeRepositorio.ObterListaDeCidadesComEstados();
        }

        [HttpGet("BuscaCidadePorId")]
        public async Task<ActionResult<Cidade>> GetBuscaEstadosPorId(int estadoId)
        {
            return await _cidadeRepositorio.ObterCidadePorId(estadoId);
        }
               
    }
}
