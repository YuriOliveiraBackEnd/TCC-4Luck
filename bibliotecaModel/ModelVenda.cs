using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bibliotecaModel
{
    public class ModelVenda
    {
        public string id_venda {  get; set; }
        public string data_venda { get; set; }
        public string horaVenda { get; set; }
        public double ValorTotal { get; set; }
        public string id_cli { get; set; }
        public string pagamento { get; set; }

        public List<ModelItemCarrinho> ItensPedido = new List<ModelItemCarrinho>();
    }
}
