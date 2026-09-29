using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webAPI_ASPNET.Data;

namespace webAPI_ASPNET.Controllers
{
    [ApiController]
    [Route("api/financeiro")]
    public class FinanceiroController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FinanceiroController(AppDbContext context)
        {
            _context = context;
        }

        // ──────────────────────────────────────────
        // GET /api/financeiro/total-geral
        // ──────────────────────────────────────────
        [HttpGet("total-geral")]
        public IActionResult TotalGeral()
        {
            var pedidosPagos = _context.Pedidos
                .Where(p => p.Status == "Pago");

            var totalReceita = pedidosPagos.Sum(p => (decimal?)p.Total) ?? 0;
            var totalPedidos = pedidosPagos.Count();
            var ticketMedio = totalPedidos > 0 ? totalReceita / totalPedidos : 0;
            var totalAguardando = _context.Pedidos
                .Where(p => p.Status == "Aguardando Pagamento")
                .Sum(p => (decimal?)p.Total) ?? 0;

            return Ok(new
            {
                totalReceita = Math.Round(totalReceita, 2),
                totalPedidos = totalPedidos,
                ticketMedio = Math.Round(ticketMedio, 2),
                totalAguardando = Math.Round(totalAguardando, 2)
            });
        }

        // ──────────────────────────────────────────
        // GET /api/financeiro/por-produto
        // ──────────────────────────────────────────
        [HttpGet("por-produto")]
        public IActionResult PorProduto()
        {
            var ranking = _context.PedidoItens
                .Include(i => i.Pedido)
                .Where(i => i.Pedido.Status == "Pago")
                .GroupBy(i => new { i.ProdutoId, i.NomeProduto })
                .Select(g => new
                {
                    produtoId = g.Key.ProdutoId,
                    nomeProduto = g.Key.NomeProduto,
                    quantidadeVendida = g.Sum(i => i.Quantidade),
                    totalArrecadado = Math.Round(g.Sum(i => i.SubTotal), 2)
                })
                .OrderByDescending(x => x.totalArrecadado)
                .ToList();

            return Ok(ranking);
        }

        // ──────────────────────────────────────────
        // GET /api/financeiro/por-periodo?de=&ate=
        // ──────────────────────────────────────────
        [HttpGet("por-periodo")]
        public IActionResult PorPeriodo([FromQuery] DateTime de, [FromQuery] DateTime ate)
        {
            var ateFim = ate.Date.AddDays(1).AddTicks(-1);

            var resultado = _context.Pedidos
                .Where(p => p.Status == "Pago"
                         && p.DataPedido >= de.Date
                         && p.DataPedido <= ateFim)
                .GroupBy(p => new { p.DataPedido.Year, p.DataPedido.Month })
                .Select(g => new
                {
                    ano = g.Key.Year,
                    mes = g.Key.Month,
                    totalPedidos = g.Count(),
                    totalArrecadado = Math.Round(g.Sum(p => p.Total), 2)
                })
                .OrderBy(x => x.ano)
                .ThenBy(x => x.mes)
                .ToList();

            return Ok(resultado);
        }

        // ──────────────────────────────────────────
        // GET /api/financeiro/pedidos-pendentes
        // ──────────────────────────────────────────
        [HttpGet("pedidos-pendentes")]
        public IActionResult PedidosPendentes()
        {
            var pendentes = _context.Pedidos
                .Where(p => p.Status == "Aguardando Pagamento")
                .OrderByDescending(p => p.DataPedido)
                .Select(p => new
                {
                    pedidoId = p.PedidoId,
                    nomeCliente = p.NomeCliente,
                    emailCliente = p.EmailCliente,
                    formaPagamento = p.FormaPagamento,
                    total = p.Total,
                    dataPedido = p.DataPedido
                })
                .ToList();

            return Ok(pendentes);
        }

        // ──────────────────────────────────────────
        // PUT /api/financeiro/confirmar-pagamento/{pedidoId}
        // ──────────────────────────────────────────
        [HttpPut("confirmar-pagamento/{pedidoId}")]
        public IActionResult ConfirmarPagamento(int pedidoId)
        {
            var pedido = _context.Pedidos.FirstOrDefault(p => p.PedidoId == pedidoId);

            if (pedido == null)
                return NotFound(new { message = "Pedido não encontrado." });

            if (pedido.Status == "Pago")
                return BadRequest(new { message = "Pedido já está pago." });

            pedido.Status = "Pago";
            _context.SaveChanges();

            return Ok(new { message = "Pagamento confirmado.", pedidoId = pedido.PedidoId });
        }
    }
}
