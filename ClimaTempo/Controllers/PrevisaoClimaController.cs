using Dominio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClimaTempo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrevisaoClimaController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string stringConexao;

        public PrevisaoClimaController(IConfiguration configuration)
        {
            _configuration = configuration;
            stringConexao = this._configuration.GetValue<string>("AzureServiceBus");
        }

        [HttpPost("EnviarQueuePreisaoClima")]
        public async Task<IActionResult> EnviarQueuePreisaoClima(Estado estado)
        {

            string queueNome = "preisaoClima";
            QueueClient cliente = new QueueClient(stringConexao, queueNome, ReceiveMode.PeekLock);
            string corpoMensagem = JsonSerializer.Serialize(estado);
            var mensagem = new Message(Encoding.UTF8.GetBytes(corpoMensagem));

            await cliente.SendAsync(mensagem);
            await cliente.CloseAsync();

            return Ok(mensagem);
        }
    }
}
