using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiServico.Models.Dtos;
using ApiServico.Models;
using ApiServico.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace ApiServico.Controllers
{
    [Route("/chamados")]
    [ApiController]
    public class ChamadoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ChamadoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos([FromQuery] string? search, [FromQuery] string? situacao)
        {
            var query = _context.Chamados.AsQueryable();
            //List<Chamado> chamados;
            if(search is not null)
            {
                query = query.Where(x=>x.Titulo.Contains(search));
                //chamados = await _context.Chamados
                //    .Where(x=> x.Titulo.Contains(search)).ToListAsync();
                //return Ok(chamados);
            }
            if(situacao is not null)
            {
                query = query.Where(x => x.Status.Contains(situacao));

            }
            //chamados = await _context.Chamados.ToListAsync();
            var chamados = await query.ToListAsync();
            return Ok(chamados);
        }
      


        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var chamado = await _context.Chamados.FirstOrDefaultAsync(x=>x.Id == id);
          return Ok(chamado);
        }


        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] ChamadoDto novoChamado)
        {
      
            var chamado = new Chamado{ Titulo = novoChamado.Titulo, Descricao = novoChamado.Descricao };
            await _context.Chamados.AddAsync(chamado);
            await _context.SaveChangesAsync();
            return Created("", chamado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] ChamadoDto atualizacaoChamado)
        {
           var chamado = await _context.Chamados.FirstOrDefaultAsync(x => x.Id == id);
            if (chamado is null)
            {
                return NotFound();
            }
            chamado.Titulo = atualizacaoChamado.Titulo;
            chamado.Descricao = atualizacaoChamado.Descricao;

            _context.Chamados.Update(chamado);
            await _context.SaveChangesAsync();

            return Ok(chamado);
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
           

            return NoContent();
        }
    }
}
