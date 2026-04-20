using Microsoft.AspNetCore.Mvc;
using webAPI_ASPNET.Data;
using webAPI_ASPNET.DTOs;
using webAPI_ASPNET.Models;

namespace webAPI_ASPNET.Controllers
{
    [ApiController]
    [Route("api/pedido")]
    public class PedidoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PedidoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CriarPedido([FromBody] PedidoCreateDto dto)
        {
            var pedido = new Pedido
            {
                NomeCliente = dto.NomeCliente,
                EmailCliente = dto.EmailCliente,
                Cep = dto.Cep,
                Rua = dto.Rua,
                Numero = dto.Numero,
                Complemento = dto.Complemento,
                FormaPagamento = dto.FormaPagamento,
                Total = dto.Total,
                Status = "Aguardando Pagamento",
                DataPedido = DateTime.Now
            };

            _context.Pedidos.Add(pedido);
            _context.SaveChanges();

            foreach (var item in dto.Itens)
            {
                _context.PedidoItens.Add(new PedidoItem
                {
                    PedidoId = pedido.PedidoId,
                    ProdutoId = item.ProdutoId,
                    NomeProduto = item.NomeProduto,
                    PrecoUnitario = item.PrecoUnitario,
                    Quantidade = item.Quantidade,
                    SubTotal = item.PrecoUnitario * item.Quantidade
                });
            }

            _context.SaveChanges();

            return Ok(new { pedidoId = pedido.PedidoId });
        }

        [HttpGet("meus-pedidos/{emailCliente}")]
        public IActionResult MeusPedidos(string emailCliente)
        {
            var pedidos = _context.Pedidos
                .Where(p => p.EmailCliente.Trim().ToLower() == emailCliente.Trim().ToLower())
                .OrderByDescending(p => p.DataPedido)
                .Select(p => new
                {
                    p.PedidoId,
                    p.DataPedido,
                    p.Status,
                    p.Total
                })
                .ToList();

            if (!pedidos.Any())
                return NotFound("Nenhum pedido encontrado.");

            return Ok(pedidos);
        }
    }
}