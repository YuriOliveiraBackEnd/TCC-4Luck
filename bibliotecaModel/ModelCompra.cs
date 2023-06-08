using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bibliotecaModel
{
    public class ModelCompra
    {
        public int id_compra { get; set; }
        public string pagamento { get; set; }
        public double valor_total { get; set; } 
        public int id_cli { get; set; }
    }
}
