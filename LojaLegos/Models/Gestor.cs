using System;
using System.ComponentModel.DataAnnotations;

namespace LojaLegos.Models
{
    public class Gestor
    {
        public Gestor()
        {
            Funcionarios = new HashSet<Funcs>();
        }

        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }

        public string NrTelemovel { get; set; }

        public string Foto { get; set; }

        public string Email { get; set; }

        public string Cargo { get; set; }

        public string UserId { get; set; }

        public ICollection<Funcs> Funcionarios { get; set; }


    }
}
