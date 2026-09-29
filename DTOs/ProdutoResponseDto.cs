namespace webAPI_ASPNET.DTOs
{
    public class ProdutoResponseDto
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public string Marca { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public string Numeracao { get; set; }
        public int Estoque { get; set; }
        public int? VendedorId { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        public int? CategoriaId { get; set; }
        public string CategoriaNome { get; set; }

        // Capa (compatibilidade com o campo antigo Produto.Imagem)
        public string ImagemPrincipal { get; set; }

        // Galeria completa, na ordem cadastrada
        public List<string> Imagens { get; set; } = new();
    }
}
