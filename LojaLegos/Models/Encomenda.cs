using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaLegos.Models
{
    public class Encomenda
    {
        public Encomenda()
        {
            ArtigoEncomendas = new HashSet<ArtigoEncomenda>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        [StringLength(100, ErrorMessage = "O {0} não pode ter mais do que {1} carateres.")]
        public string Total { get; set; }
        public DateTime Data { get; set; }

        [ForeignKey(nameof(Cliente))]
        public int ClienteFK { get; set; }
        public Cliente Cliente { get; set; }

        public ICollection<ArtigoEncomenda> ArtigoEncomendas { get; set; }
    }
}