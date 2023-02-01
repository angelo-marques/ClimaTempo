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

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string UF { get; private set; }

        public void AcionarDadosEstado(int id, string nome, string uf)
        {
            Id = id;
            Nome = nome;
            UF = uf.ToUpper();
        }
    }
}
