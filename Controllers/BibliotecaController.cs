using Biblioteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace Biblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BibliotecaController : ControllerBase
    {
        private static List<Livro> livrosDisponiveis = new List<Livro>
        {
            new Livro { Id = 1, Titulo = "Dom Casmurro", Autor = "Machado de Assis", AnoLancamento = 1899, QuantidadeDisponivel = 2 },
            new Livro { Id = 2, Titulo = "Memórias Póstumas de Brás Cubas", Autor = "Machado de Assis", AnoLancamento = 1881, QuantidadeDisponivel = 3 },
            new Livro { Id = 3, Titulo = "Grande Sertão: Veredas", Autor = "João Guimarães Rosa", AnoLancamento = 1956, QuantidadeDisponivel = 4 },
            new Livro { Id = 4, Titulo = "O Cortiço", Autor = "Aluísio Azevedo", AnoLancamento = 1890, QuantidadeDisponivel = 4 },
            new Livro { Id = 5, Titulo = "Iracema", Autor = "José de Alencar", AnoLancamento = 1865, QuantidadeDisponivel = 1 },
            new Livro { Id = 6, Titulo = "Macunaíma", Autor = "Mário de Andrade", AnoLancamento = 1928, QuantidadeDisponivel = 11 },
            new Livro { Id = 7, Titulo = "Capitães da Areia", Autor = "Jorge Amado", AnoLancamento = 1937, QuantidadeDisponivel = 2 },
            new Livro { Id = 8, Titulo = "Vidas Secas", Autor = "Graciliano Ramos", AnoLancamento = 1938, QuantidadeDisponivel = 9 },
            new Livro { Id = 9, Titulo = "A Moreninha", Autor = "Joaquim Manuel de Macedo", AnoLancamento = 1844, QuantidadeDisponivel = 2 },
            new Livro { Id = 10, Titulo = "O Tempo e o Vento", Autor = "Érico Veríssimo", AnoLancamento = 1949, QuantidadeDisponivel = 1 },
            new Livro { Id = 11, Titulo = "A Hora da Estrela", Autor = "Clarice Lispector", AnoLancamento = 1977, QuantidadeDisponivel = 1 },
            new Livro { Id = 12, Titulo = "O Quinze", Autor = "Rachel de Queiroz", AnoLancamento = 1930, QuantidadeDisponivel = 1 },
            new Livro { Id = 13, Titulo = "Menino do Engenho", Autor = "José Lins do Rego", AnoLancamento = 1932, QuantidadeDisponivel = 5 },
            new Livro { Id = 14, Titulo = "Sagarana", Autor = "João Guimarães Rosa", AnoLancamento = 1946, QuantidadeDisponivel = 3 },
            new Livro { Id = 15, Titulo = "Fogo Morto", Autor = "José Lins do Rego", AnoLancamento = 1943, QuantidadeDisponivel = 1 }
        };

        [HttpGet]
        public ActionResult<List<Livro>> GetLivros()
        {
            return Ok(livrosDisponiveis);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Livro> GetLivroById(int id)
        {
            var livro = livrosDisponiveis.FirstOrDefault(l => l.Id == id);
            if (livro == null)
            {
                return NotFound();
            }
            return Ok(livro);
        }

        [HttpPost("locar/{id:int}")]
        public ActionResult LocarLivro(int id, Locacao novaLocacao)
        {
            var livro = livrosDisponiveis.FirstOrDefault(l => l.Id == id);
            if (livro == null)
            {
                return NotFound();
            }

            if (livro.QuantidadeDisponivel <= 0)
            {
                return BadRequest("O livro não está disponível.");
            }

            livro.QuantidadeDisponivel--;
            novaLocacao.Id = livro.Locacoes.Count + 1;
            livro.Locacoes.Add(novaLocacao);

            return Ok(new { Message = "Livro alugado com sucesso!", Livro = livro, Locacao = novaLocacao });
        }

        [HttpPost("devolver/{id:int}")]
        public ActionResult DevolverLivro(int id, [FromBody] int locacaoId)
        {
            var livro = livrosDisponiveis.FirstOrDefault(l => l.Id == id);
            if (livro == null)
            {
                return NotFound("Livro não encontrado.");
            }

            var locacao = livro.Locacoes.FirstOrDefault(l => l.Id == locacaoId);
            if (locacao == null)
            {
                return NotFound("Locação não encontrada.");
            }

            livro.QuantidadeDisponivel++;
            livro.Locacoes.Remove(locacao);

            return Ok(new { Message = "Livro devolvido com sucesso!", Livro = livro });
        }
    }
}
