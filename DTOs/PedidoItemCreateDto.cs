namespace webAPI_ASPNET.DTOs
{
    public class PedidoItemCreateDto
    {
        public int ProdutoId { get; set; }
        public string NomeProduto { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int Quantidade { get; set; }
    }
}