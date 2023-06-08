using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;



namespace PlanosPets.ViewModels
{
    public class LoginViewModel
    {


       
        public int Id_cli { get; set; }
        [Required(ErrorMessage = "Informe o email")]
        [MaxLength(50, ErrorMessage = "o email deve ter até 50 caracteres")]
        public string Email { get; set; }



        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Informe a semha")]
        public string senha { get; set; }
    }
}