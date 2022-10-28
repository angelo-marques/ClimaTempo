namespace Dominio.Models
{
    public class Cidade
    {
        public Cidade() { }

        public Cidade(int id, string nome, int estadoId, Estado estado)
        {
            Id = id;
            Nome = nome;
            EstadoId = estadoId;
            Estado = estado;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public int EstadoId { get; set; }

        public Estado Estado { get; set; }
    }
}
