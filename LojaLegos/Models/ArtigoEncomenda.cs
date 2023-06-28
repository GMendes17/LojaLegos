using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaLegos.Models
{
    public class ArtigoEncomenda
    {
        [Key]
        
        public int Id { get; set; }

        public int Quantidade { get; set; }

        [ForeignKey(nameof(Artigo))]
        public int ArtigoId { get; set; }
        public Artigo Artigo { get; set; }

        [ForeignKey(nameof(Encomenda))]
        public int EncomendaId { get; set; }
        public Encomenda Encomenda { get; set; }
    }
}