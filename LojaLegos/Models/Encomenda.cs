using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaLegos.Models
{
    public class Encomenda
    {
        public Encomenda()
        {
            Artigos = new HashSet<Artigo>();
        }

        [Key]
        public int Id { get; set; }

        public string Total { get; set; }

        public DateTime Data {get; set; }

        [ForeignKey(nameof(Cliente))]
        public int ClienteFK {get; set; }
        public Cliente Cliente { get; set; }

        public ICollection<Artigo> Artigos { get; set; }




    }
}
