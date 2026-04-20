namespace webAPI_ASPNET.Models
{
    public class PedidoItem
    {
        public int PedidoItemId { get; set; }

        public int PedidoId { get; set; }
        public int ProdutoId { get; set; }

        public string NomeProduto { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int Quantidade { get; set; }
        public decimal SubTotal { get; set; }

        public Pedido Pedido { get; set; }
    }
}