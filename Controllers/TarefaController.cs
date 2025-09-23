using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiServico.Models.Dtos;
using ApiServico.Models;

namespace ApiServico.Controllers
{
    [Route("/Tarefas")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private static List<Tarefas> _listaTarefas = new List<Tarefas>
        {

        };
        [HttpGet]
        public IActionResult BuscarTodos()
        {
            return Ok(_listaTarefas);
        }
        private static int _proximoId = 0;
        [HttpPost]
        public IActionResult Criar([FromBody] TarefasDto novaTarefa)
        {

            var tarefa = new Tarefas
            {
                Id = _proximoId++,
                Descricao = novaTarefa.Descricao,
                Status = "Aberto"
            };

            _listaTarefas.Add(tarefa);

            return Created("", tarefa);
        }
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var tarefa = _listaTarefas.FirstOrDefault(item => item.Id == id);

            if (tarefa == null)
            {
                return NotFound();
            }

            return Ok(tarefa);
        }


        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] TarefasDto novoChamado)
        {
            var tarefa = _listaTarefas.FirstOrDefault(item => item.Id == id);

            if (tarefa == null)
            {
                return NotFound();
            }


            tarefa.Descricao = novoChamado.Descricao;

            return Ok(tarefa);
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var chamado = _listaTarefas.FirstOrDefault(item => item.Id == id);

            if (chamado == null)
            {
                return NotFound();
            }

            _listaTarefas.Remove(chamado);

            return NoContent();
        }
        [HttpPut("{id}/Fechamento")]
        public IActionResult FecharChamado(int id)
        {
            var tarefa = _listaTarefas.FirstOrDefault(t => t.Id == id);
            if (tarefa is null)
            {
                return NotFound();
            }

            tarefa.Status = "Fechado";

            return Ok(tarefa);
        }
    }
}
