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

        // Lista de locações associadas ao livro
        public List<Locacao> Locacoes { get; set; } = new List<Locacao>();
    }

    public class Locacao
    {
        public int Id { get; set; }  // ID da locação
        public string NomeLocatario { get; set; } = string.Empty;  // Nome do locatário
        public int AnoNascimento { get; set; }  // Ano de nascimento do locatário
        public DateTime DataLocacao { get; set; } = DateTime.Now;  // Data da locação
    }
}
