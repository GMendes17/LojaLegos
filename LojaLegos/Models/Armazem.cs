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

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        [StringLength(100, ErrorMessage = "O {0} não pode ter mais do que {1} carateres.")]
        [RegularExpression("[A-ZÂÓÍa-záéíóúàèìòùâêîôûãõäëïöüñç '-]+", ErrorMessage = "Só pode escrever letras no {0}")]
        public string Local { get; set; }

        [ForeignKey(nameof(Responsavel))]
        public int ResponsavelFK { get; set; }
        public Funcs Responsavel { get; set; }
        public ICollection<Artigo> Artigos { get; set; }


    }
}
