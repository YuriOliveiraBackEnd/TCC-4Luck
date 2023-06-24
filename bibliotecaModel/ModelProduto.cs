using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bibliotecaModel
{
    public class ModelProduto
    {
        [DisplayName("Código do produto")]
        public int id_prod { get; set; }
        [DisplayName("Nome do produto")]
        [Required(ErrorMessage = "insira o nome do produto")]
        public string nome_prod { get; set; }
    
        [DisplayName("Preço do produto")]
        [Required(ErrorMessage = "insira o preço do produto")]
        public double valor_unitario { get; set; }
        [DisplayName("Quantidade")]
        [Required(ErrorMessage = "insira a quantidade")]
        public int quant { get; set; }
        [DisplayName("Descrição do produto")]
        [Required(ErrorMessage = "insira a descrição do produto ")]
        public string desc_prod { get; set; }
        [DisplayName("foto do produto")]
  
        public string ft_prod { get; set; }
        [DisplayName("Código da Categoria")]
        [Required(ErrorMessage = "insira a categoria")]

        public int id_categoria { get; set; }
        [DisplayName("Código do funcionário")]
        [Required(ErrorMessage = "insira seu código")]
        public int id_func { get; set; }

        public string tipo_cate { get; set; }
    }
}
