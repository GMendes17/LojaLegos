using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaLegos.Models
{
    public class Armazem
    {
        public Armazem()
        {
            Artigos = new HashSet<Artigo>();
        }

        [Key]
        public int Id { get; set; }

        public string Local { get; set; }

        [ForeignKey(nameof(Responsavel))]
        public int ResponsavelFK { get; set; }
        public Funcs Responsavel { get; set; }
        public ICollection<Artigo> Artigos { get; set; }


    }
}
