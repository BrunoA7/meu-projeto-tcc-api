using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webAPI_ASPNET.Data;

namespace webAPI_ASPNET.Controllers
{
    [ApiController]
    [Route("api/categoria")]
    public class CategoriaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriaController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/categoria -> usado pelo select do formulário de cadastro
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categorias = await _context.Categorias
                .OrderBy(c => c.Nome)
                .ToListAsync();

            return Ok(categorias);
        }
    }
}
