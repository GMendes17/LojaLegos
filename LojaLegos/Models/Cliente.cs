using System.ComponentModel.DataAnnotations;

namespace LojaLegos.Models
{
    public class Cliente
    {

        public Cliente()
        {
            Encomendas = new HashSet<Encomenda>();
        }

        [Key]
        public int Id { get; set; }

        public string PrimeiroNome { get; set; }

        public string Apelido { get; set; }

        public string Morada { get; set; }  

        public string CodPostal   { get; set; } 

        public string Cidade   { get; set; }

        public string País    { get; set; }

        public string Email { get; set; }

        public string NrTelemovel { get; set; }

        public string NrContribuinte {get; set; }

        public ICollection<Encomenda> Encomendas { get; set; }

    }
}
