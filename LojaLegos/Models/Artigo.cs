using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaLegos.Models
{
    public class Artigo
    {
        public Artigo()
        {
            ArtigoEncomendas = new HashSet<ArtigoEncomenda>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        [RegularExpression("[0-9]{1,8}")]
        public string Nr { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        [StringLength(30, ErrorMessage = "O {0} não pode ter mais do que {1} carateres.")]
        [RegularExpression("[A-ZÂÓÍa-záéíóúàèìòùâêîôûãõäëïöüñç '-]+", ErrorMessage = "Só pode escrever letras no {0}")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        [StringLength(30, ErrorMessage = "O {0} não pode ter mais do que {1} carateres.")]
        [RegularExpression("[A-ZÂÓÍa-záéíóúàèìòùâêîôûãõäëïöüñç '-]+", ErrorMessage = "Só pode escrever letras no {0}")]
        public string Nome { get; set; }

        public decimal Preco { get; set; }

        public string Foto { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        [RegularExpression("[0-9]{1,8}")]
        public string NrPecas { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        [StringLength(100, ErrorMessage = "O {0} não pode ter mais do que {1} carateres.")]
        [RegularExpression("[A-ZÂÓÍa-záéíóúàèìòùâêîôûãõäëïöüñç0-9 '-]+", ErrorMessage = "Só pode escrever letras no {0}")]
        public string Detalhes { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        [RegularExpression("[0-9]{1,8}")]
        public string Stock { get; set; }

        [ForeignKey(nameof(Armazem))]
        public int ArmazemFK { get; set; }
        public Armazem Armazem { get; set; }

        public ICollection<ArtigoEncomenda> ArtigoEncomendas { get; set; }
    }
}