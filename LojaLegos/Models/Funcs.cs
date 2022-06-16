using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaLegos.Models
{
    public class Funcs
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }

        public string NrTelemovel { get; set; }

        public string Email { get; set; }

        public string Cargo { get; set; }


        [ForeignKey(nameof(Gestor))]
        public int ChefeFK { get; set; }
        public Gestor Gestor { get; set; }


    }
}
