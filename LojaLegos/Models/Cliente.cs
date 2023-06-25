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

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        [StringLength(30, ErrorMessage = "O {0} não pode ter mais do que {1} carateres.")]
        [RegularExpression("[A-ZÂÓÍa-záéíóúàèìòùâêîôûãõäëïöüñç '-]+", ErrorMessage = "Só pode escrever letras no {0}")]
        public string PrimeiroNome { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        [StringLength(30, ErrorMessage = "O {0} não pode ter mais do que {1} carateres.")]
        [RegularExpression("[A-ZÂÓÍa-záéíóúàèìòùâêîôûãõäëïöüñç '-]+", ErrorMessage = "Só pode escrever letras no {0}")]
        public string Apelido { get; set; }

        public string Morada { get; set; }  

        public string CodPostal   { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        [StringLength(30, ErrorMessage = "O {0} não pode ter mais do que {1} carateres.")]
        [RegularExpression("[A-ZÂÓÍa-záéíóúàèìòùâêîôûãõäëïöüñç '-]+", ErrorMessage = "Só pode escrever letras no {0}")]
        public string Cidade   { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        [StringLength(30, ErrorMessage = "O {0} não pode ter mais do que {1} carateres.")]
        [RegularExpression("[A-ZÂÓÍa-záéíóúàèìòùâêîôûãõäëïöüñç '-]+", ErrorMessage = "Só pode escrever letras no {0}")]
        public string País    { get; set; }

        public string Email { get; set; }

        [StringLength(9, MinimumLength = 9, ErrorMessage = "O {0} tem de ter 9 carateres.")]
        [RegularExpression("[9][0-9]{8}", ErrorMessage = "O {0} deve começar por 9 seguido de 8 digitos numéricos.")]
        public string NrTelemovel { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "O {0} tem de ter 9 carateres.")]
        [RegularExpression("[123578][0-9]{8}", ErrorMessage = "O {0} deve começar por 1,2,3,5,7,8 seguido de 8 digitos numéricos.")]
        public string NrContribuinte {get; set; }


        // *************************************************
        /// <summary>
        /// Chave de ligação entre a base de dados da autenticação
        /// e a base de dados da Loja
        /// </summary>
        public string UserId { get; set; }





        public ICollection<Encomenda> Encomendas { get; set; }

    }
}
