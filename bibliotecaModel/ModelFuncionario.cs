using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;
using System.Web.Mvc;

namespace bibliotecaModel
{
    public class ModelFuncionario
    {
        [DisplayName("Id do funcionário")]
        public int id_func { get; set; }

        [DisplayName("Nome ")]
        [MaxLength(80, ErrorMessage = "O nome deve conter no maximo 80 caracteres")]
        [Required(ErrorMessage = "insira seu nome")]
        public string nome_func { get; set; }


        [DisplayName("Email ")]
        [MaxLength(50, ErrorMessage = "o Email deve conter no maximo 50 caracteres")]
        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\-][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$", ErrorMessage = "Digite um Email válido")]
        [Required(ErrorMessage = "insira seu email")]
        public string email_func { get; set; }


        [DisplayName("CPF")]
        [MaxLength(11, ErrorMessage = "O CPF deve conter 11 caracteres")]
        [MinLength(11, ErrorMessage = "O CPF deve conter 11 caracters")]
        [RegularExpression(@"^[0-9]+${11,11}", ErrorMessage = "Somente números")]
        [Required(ErrorMessage = "insira seu CPF")]
        public string CPF_func { get; set; }


        [RegularExpression(@"^[0-9]+${11,11}", ErrorMessage = "Somente números")]
        [DisplayName("Cep")]
        [MaxLength(8, ErrorMessage = "O CEP deve conter 8 caracters")]
        [MinLength(8, ErrorMessage = "O CEP deve conter 8 caracters")]
        [Required(ErrorMessage = "insira seu cep")]
        public string cep_func { get; set; }


        [DisplayName("Número da casa")]
        [Required(ErrorMessage = "insira o número da casa")]
        [RegularExpression(@"^[0-9]+${11,11}", ErrorMessage = "Somente números")]
        public string num_func { get; set; }


        [DisplayName("Logradouro")]
        [Required(ErrorMessage = "insira seu logradouro")]
        public string logradouro_func { get; set; }

        [DisplayName("Data de nascimento")]
        [Required(ErrorMessage = "insira sua data de nascimento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public string nasc_func { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Senha")]
        [Required(ErrorMessage = "insira sua senha")]
        public string senha_func { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Confirmar senha")]
        [Required(ErrorMessage = "Confirme sua senha")]
        [Compare(nameof(senha_func), ErrorMessage = "Senhas digitadas não conferem.")]
        public string confirmar_senha { get; set; }

        [DisplayName("Telefone ")]
        [MaxLength(11, ErrorMessage = "O telefone deve conter 11 caracteres")]
        [MinLength(11, ErrorMessage = "O telefone deve conter 11 caracteres")]
        [RegularExpression(@"^[0-9]+${11,11}", ErrorMessage = "Somente números")]
        [Required(ErrorMessage = "insira seu telefone")]
        public string tel_func { get; set; }
        [DisplayName("bairro")]
        [Required(ErrorMessage = "insira seu bairro")]
        public string bairro { get; set; }
        [DisplayName("cidade")]
        [Required(ErrorMessage = "insira seu cidade")]
        public string cidade { get; set; }
        [DisplayName("uf")]
        [Required(ErrorMessage = "insira seu uf")]
        public string uf { get; set; }




    }
}
