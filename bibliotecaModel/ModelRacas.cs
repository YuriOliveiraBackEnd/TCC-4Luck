using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bibliotecaModel
{
    public class ModelRacas
    {
        [DisplayName("Código da raça")]
        [Required(ErrorMessage = "insira a descrição do produto ")]
        public int id_raca { get; set; }
        [DisplayName("Nome da raça")]
        [Required(ErrorMessage = "insira a descrição do produto ")]
        public string nome_raca { get; set; }
        [DisplayName("Foto da raça")]
        [Required(ErrorMessage = "insira a descrição do produto ")]
        public string ft_raca { get; set; }
        
        [DisplayName("Tipo animal")]
        [Required(ErrorMessage = "insira se o animal é um gato ou cachoro ")]
        public string tipo_animal { get; set; }
       
        [DisplayName("Código funcionário")]
        public string id_func { get; set; }
    }
}
