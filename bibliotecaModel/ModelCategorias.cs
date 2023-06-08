using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bibliotecaModel
{
    public class ModelCategorias

    {
       
        public int id_categoria { get; set; }
        [DisplayName("Nome da categoria")]
        [Required(ErrorMessage = "insira o nome da categoria")]
        public string nome_categoria { get; set; }
        [DisplayName("Descrição da categoria")]
        [Required(ErrorMessage = "insira a descrição da categoria")]
        public string desc_categoria { get; set; }
    }
}
    