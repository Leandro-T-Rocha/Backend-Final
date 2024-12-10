using System.Collections.Generic;
using System;

namespace Biblioteca.Models
{
    public class Livro
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Autor { get; set; } = string.Empty;

        public int AnoLancamento { get; set; }

        public int QuantidadeDisponivel { get; set; }

        public List<Locacao> Locacoes { get; set; } = new List<Locacao>();
    }

    public class Locacao
    {
        public int Id { get; set; }
        public string NomeLocatario { get; set; } = string.Empty;
        public int AnoNascimento { get; set; }
        public DateTime DataLocacao { get; set; } = DateTime.Now;
    }
}
