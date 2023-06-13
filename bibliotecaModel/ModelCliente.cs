using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace bibliotecaModel
{
    public class ModelCliente
    {
        [DisplayName("Id do Cliente")]
        public int id_cli { get; set; }

        [DisplayName("Nome Completo")]
        [MaxLength(80, ErrorMessage = "O nome deve conter no maximo 80 caracteres")]
        [Required(ErrorMessage = "insira seu nome")]
        public string nome_cli { get; set; }


        [DisplayName("Email")]
        [MaxLength(50, ErrorMessage = "o Email deve conter no maximo 15 caracteres")]
        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\-][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$", ErrorMessage = "Digite um Email válido")]
        [Required(ErrorMessage = "insira seu email")]
        public string email_cli { get; set; }


        [DisplayName("CPF")]
        [MaxLength(11, ErrorMessage = "O CPF deve conter 11 caracteres")]
        [MinLength(11, ErrorMessage = "O CPF deve conter 11 caracters")]
        [RegularExpression(@"^[0-9]+${11,11}", ErrorMessage = "Somente números")]
        [Required(ErrorMessage = "insira seu CPF")]
        public string CPF_cli { get; set; }


        [RegularExpression(@"^[0-9]+${11,11}", ErrorMessage = "Somente números")]
        [DisplayName("CEP")]
        [MaxLength(8, ErrorMessage = "O cep deve conter 8 caracteres")]
        [MinLength(8, ErrorMessage = "O cep deve conter 8 caracters")]
        [Required(ErrorMessage = "insira seu cep")]
        public string cep_cli { get; set; }


        [DisplayName("Número da casa")]
        [Required(ErrorMessage = "insira o número da casa")]
        [RegularExpression(@"^[0-9]+${11,11}", ErrorMessage = "Somente números")]
        public string num_cli { get; set; }


        [DisplayName("Logradouro")]
        [Required(ErrorMessage = "insira seu logradouro")]
        public string logradouro_cli { get; set; }
        [DisplayName("bairro")]
        [Required(ErrorMessage = "insira seu bairro")]
        public string bairro { get; set; }
        [DisplayName("cidade")]
        [Required(ErrorMessage = "insira seu cidade")]
        public string cidade { get; set; }
        [DisplayName("uf")]
        [Required(ErrorMessage = "insira seu uf")]
        public string uf { get; set; }

        [DisplayName("Data de nascimento")]
        [Required(ErrorMessage = "insira sua data de nascimento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public string nasc_cli { get; set; }

   
        [DataType(DataType.Password)]
        [DisplayName("Senha ")]
        [Required(ErrorMessage = "insira sua senha")]
        public string senha_cli { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Confirmar senha")]
        [Required(ErrorMessage = "Confirme sua senha")]
        [Compare(nameof(senha_cli), ErrorMessage = "Senhas digitadas não conferem.")]
        public string confirmar_senha { get; set; }

        [DisplayName("Telefone ")]
        [MaxLength(11, ErrorMessage = "O telefone deve conter 11 caracteres")]
        [MinLength(11, ErrorMessage = "O telefone deve conter 11 caracteres")]
        [RegularExpression(@"^[0-9]+${11,11}", ErrorMessage = "Somente números")]
        [Required(ErrorMessage = "insira seu telefone")]
        public string tel_cli { get; set; }

        [DisplayName("Código pet")]
        public int id_pet { get; set; }
        [DisplayName("Nome pet")]
        [Required(ErrorMessage = "insira o nome do seu pet")]
        public string nome_pet { get; set; }
        [DisplayName("foto pet")]
        public string ft_pet { get; set; }
        [DisplayName("Data de nascimento")]
        [Required(ErrorMessage = "insira a data de nascimento do seu pet")]
        public string nasc_pet { get; set; }

        [DisplayName("RGA")]
        [Required(ErrorMessage = "insira o RGA do pet")]
        public string RGA { get; set; }

        [DisplayName("Código raça")]
        public int id_raca { get; set; }
        public string id_gato { get; set; }
        public string id_cachorro { get; set; }

    }
}
