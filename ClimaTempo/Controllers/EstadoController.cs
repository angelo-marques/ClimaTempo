using Azure.Messaging.ServiceBus;
using Dominio.Models;
using Infraestrutura.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClimaTempo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly IEstadoRepositorio _estadoRepositorio;
        private readonly ServiceBusClient _serviceBusClient;

        public EstadoController(IEstadoRepositorio estadoRepositorio, ServiceBusClient cliente)
        {
            this._estadoRepositorio = estadoRepositorio;
            this._serviceBusClient = cliente;
        }

        [HttpPost("CadastroEstadoViaFila")]
        public async Task<IActionResult> CadastroEstadoViaFila([FromBody] Estado estado)
        {
            var verificaSeJaEstaCadstrado = _estadoRepositorio.GetListaDeEstados().Where(
                        x => x.UF.ToUpper().Equals(estado.UF.ToUpper())
                    ).ToList();

            if (!verificaSeJaEstaCadstrado.Any())
            {
                string queueNome = "estado";
                var enviar = _serviceBusClient.CreateSender(queueNome);
                string corpoMensagem = JsonSerializer.Serialize(estado);
                var mensagem = new ServiceBusMessage(corpoMensagem);
                await enviar.SendMessageAsync(mensagem).ConfigureAwait(false);
                await enviar.CloseAsync();

                return Ok("Cadastrado com sucesso!");

            }
            return Ok("Já esta cadastrado na base de dados.");

        }

        [HttpGet("BuscaListaDeEstados")]
        public List<Estado> GetBuscaListaDeEstados()
        {
            return _estadoRepositorio.GetListaDeEstados();
        }

        [HttpGet("GetBuscaEstadosPorId")]
        public async Task<ActionResult<Estado>> GetBuscaEstadosPorId(int estadoId)
        {
            return await _estadoRepositorio.GetEstadoPorId(estadoId); 
        }
    }
}
