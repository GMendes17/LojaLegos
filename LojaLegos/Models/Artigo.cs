using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaLegos.Models
{
    public class Artigo

    {

        public Artigo()
        {
            Encomendas = new HashSet<Encomenda>();
        }

        [Key] 
        public int Id { get; set; }

        public string Nr { get; set; }

        public string Tipo { get; set; }

        public string Nome { get; set; }

        public decimal Preco { get; set; }

        public string Foto { get; set; }

        public string NrPecas { get; set; }

        public string Detalhes { get; set; }

        public string Stock { get; set; }

        [ForeignKey(nameof(Armazem))]
        public int ArmazemFK { get; set; }
        public Armazem Armazem { get; set; }


        public ICollection<Encomenda> Encomendas { get; set; }

    }
}
