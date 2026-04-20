namespace webAPI_ASPNET.Models
{
    public class Pedido
    {
        public int PedidoId { get; set; }

        public string NomeCliente { get; set; }
        public string EmailCliente { get; set; }

        public string Cep { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }

        public string FormaPagamento { get; set; }
        public decimal Total { get; set; }

        public string Status { get; set; }
        public DateTime DataPedido { get; set; }

        public ICollection<PedidoItem> Itens { get; set; }
    }
}