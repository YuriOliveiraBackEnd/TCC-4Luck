using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bibliotecaModel
{
    public class ModelFornecedor
    {
        [DisplayName("Código Fornecedor")]
        public int id_forn { get; set; }
        [DisplayName("Nome")]
        [Required(ErrorMessage = "Insira o nome do forncedor")]
        public string nome_forn { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage = "Digite o email do forncedor")]
        [MaxLength(50, ErrorMessage = "o Email deve conter no maximo 50 caracteres")]
        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\-][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$", ErrorMessage = "Digite um Email válido")]
        public string email_forn { get; set; }
        [DisplayName("CNPJ")]
        [RegularExpression(@"^[0-9]+${11,11}", ErrorMessage = "Somente números")]
        [MaxLength(14, ErrorMessage = "O CNPJ deve conter 14 caracteres")]
        [MinLength(14, ErrorMessage = "O CNPJ deve conter 14 caracters")]
        [Required(ErrorMessage = "Insira o CNPJ do forncedor")]
        public string CNPJ_forn { get; set; }
        [DisplayName("Cep")]
        [RegularExpression(@"^[0-9]+${11,11}", ErrorMessage = "Somente números")]
        [Required(ErrorMessage = "Insira o Cep do forncedor")]
        [MaxLength(8, ErrorMessage = "O CEP deve conter 8 caracters")]
        [MinLength(8, ErrorMessage = "O CEP deve conter 8 caracters")]
        public string cep_forn { get; set; }
        [DisplayName("Número do Local")]
        [Required(ErrorMessage = "Insira o número do Local")]
        public int num_forn { get; set; }
        [DisplayName("Logradouro")]
        public string logradouro_forn { get; set; }
        [DisplayName("Telefone do Fornecedor")]
        [MaxLength(11, ErrorMessage = "O telefone deve conter 11 caracteres")]
        [MinLength(11, ErrorMessage = "O telefone deve conter 11 caracteres")]
        [RegularExpression(@"^[0-9]+${11,11}", ErrorMessage = "Somente números")]
        [Required(ErrorMessage = "Insira o telefone do Local")]
        public string tel_forn { get; set; }
        [DisplayName("Código do produto")]
        [Required(ErrorMessage = "Insira o código  do produto")]
        public int id_prod { get; set; }
    }
}
