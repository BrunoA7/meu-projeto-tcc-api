using System.ComponentModel.DataAnnotations;

namespace webAPI_ASPNET.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }

        [Required, MaxLength(200)]
        public string Nome { get; set; }

        [Required]
        public decimal Preco { get; set; }

        public string Imagem { get; set; }

        [MaxLength(100)]
        public string Marca { get; set; }

        [MaxLength(500)]
        public string Descricao { get; set; }

        [MaxLength(200)]
        public string Numeracao { get; set; }

        public int Estoque { get; set; }

        public int? VendedorId { get; set; }

        public int? CategoriaId { get; set; }

        public bool Ativo { get; set; } = true;

        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }

    public class Categoria
    {
        public int CategoriaId { get; set; }

        [Required, MaxLength(100)]
        public string Nome { get; set; }

        [MaxLength(300)]
        public string Descricao { get; set; }
    }

    public class ProdutoImagem
    {
        public int ProdutoImagemId { get; set; }

        public int ProdutoId { get; set; }

        [Required, MaxLength(300)]
        public string Caminho { get; set; }

        public int Ordem { get; set; }

        public bool Principal { get; set; }
    }
}