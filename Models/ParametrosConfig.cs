namespace LimisInsight.Models
{
    public class ParametrosConfig
    {
        public int Id { get; set; }
        public string ParametroNome { get; set; }
        public string ParametroValor { get; set; }
        public string ParametroTipo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime DataAtualizacao { get; set; } = DateTime.Now;
    }

}
