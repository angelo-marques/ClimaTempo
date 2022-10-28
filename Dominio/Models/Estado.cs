namespace Dominio.Models
{
    public class Estado
    {
        public Estado() { }

        public Estado(int id, string nome, string uF)
        {
            Id = id;
            Nome = nome;
            UF = uF;
        }

        public int Id { get; set; }
        public string Nome { get;  set; }
        public string UF { get; set; }
    }
}
