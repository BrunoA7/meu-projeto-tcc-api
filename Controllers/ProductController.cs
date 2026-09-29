using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using webAPI_ASPNET.Data;
using webAPI_ASPNET.DTOs;
using webAPI_ASPNET.Models;

namespace webAPI_ASPNET.Controllers
{
    [ApiController]
    [Route("api/produto")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/produto -> vitrine pública, só produtos ativos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var produtos = await Projetar(_context.Produtos.Where(p => p.Ativo))
                .OrderByDescending(p => p.DataCadastro)
                .ToListAsync();

            return Ok(produtos);
        }

        // GET api/produto/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var produto = await Projetar(_context.Produtos.Where(p => p.ProdutoId == id))
                .FirstOrDefaultAsync();

            if (produto == null) return NotFound();
            return Ok(produto);
        }

        // GET api/produto/meus-produtos -> inclui inativos, é o painel do vendedor
        [Authorize(Roles = "Vendedor")]
        [HttpGet("meus-produtos")]
        public async Task<IActionResult> GetMeusProdutos()
        {
            var vendedorId = ObterVendedorId();
            var produtos = await Projetar(_context.Produtos.Where(p => p.VendedorId == vendedorId))
                .OrderByDescending(p => p.DataCadastro)
                .ToListAsync();

            return Ok(produtos);
        }

        // POST api/produto
        [Authorize(Roles = "Vendedor")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProdutoCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(dto.Nome))
                return BadRequest("Informe o nome do produto.");

            if (dto.Imagens == null || !dto.Imagens.Any())
                return BadRequest("Envie ao menos uma imagem do produto.");

            var produto = new Produto
            {
                Nome = dto.Nome,
                Marca = dto.Marca,
                Preco = dto.Preco,
                Estoque = dto.Estoque,
                Numeracao = dto.Numeracao,
                Descricao = dto.Descricao,
                CategoriaId = dto.CategoriaId,
                Imagem = dto.Imagens.First(),
                VendedorId = ObterVendedorId(),
                Ativo = true,
                DataCadastro = DateTime.Now
            };

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            var imagens = dto.Imagens.Select((caminho, indice) => new ProdutoImagem
            {
                ProdutoId = produto.ProdutoId,
                Caminho = caminho,
                Ordem = indice,
                Principal = indice == 0
            });

            _context.ProdutoImagens.AddRange(imagens);
            await _context.SaveChangesAsync();

            var criado = await Projetar(_context.Produtos.Where(p => p.ProdutoId == produto.ProdutoId))
                .FirstOrDefaultAsync();

            return Ok(criado);
        }

        // PUT api/produto/5/ativo -> permite o vendedor pausar/reativar o anúncio
        [Authorize(Roles = "Vendedor")]
        [HttpPut("{id}/ativo")]
        public async Task<IActionResult> AlterarAtivo(int id, [FromBody] bool ativo)
        {
            var vendedorId = ObterVendedorId();
            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.ProdutoId == id);

            if (produto == null) return NotFound();
            if (produto.VendedorId != vendedorId) return Forbid();

            produto.Ativo = ativo;
            await _context.SaveChangesAsync();

            return Ok(new { produto.ProdutoId, produto.Ativo });
        }

        private IQueryable<ProdutoResponseDto> Projetar(IQueryable<Produto> query)
        {
            return query.Select(p => new ProdutoResponseDto
            {
                ProdutoId = p.ProdutoId,
                Nome = p.Nome,
                Marca = p.Marca,
                Preco = p.Preco,
                Descricao = p.Descricao,
                Numeracao = p.Numeracao,
                Estoque = p.Estoque,
                VendedorId = p.VendedorId,
                DataCadastro = p.DataCadastro,
                Ativo = p.Ativo,
                CategoriaId = p.CategoriaId,
                CategoriaNome = _context.Categorias
                    .Where(c => c.CategoriaId == p.CategoriaId)
                    .Select(c => c.Nome)
                    .FirstOrDefault(),
                ImagemPrincipal = p.Imagem,
                Imagens = _context.ProdutoImagens
                    .Where(i => i.ProdutoId == p.ProdutoId)
                    .OrderBy(i => i.Ordem)
                    .Select(i => i.Caminho)
                    .ToList()
            });
        }

        private int? ObterVendedorId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            return claim != null && int.TryParse(claim.Value, out var id) ? id : null;
        }
    }
}
