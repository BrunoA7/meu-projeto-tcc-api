namespace webAPI_ASPNET.DTOs
{
    public class ProdutoCreateDto
    {
        public string Nome { get; set; }
        public string Marca { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public string Numeracao { get; set; }
        public string Descricao { get; set; }
        public int? CategoriaId { get; set; }

        // Caminhos (URLs relativas) das imagens já salvas pelo front. A primeira é usada como capa.
        public List<string> Imagens { get; set; } = new();
    }
}
