using bibliotecaBanco;
using bibliotecaModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bibliotecaDAO
{
    public class ItemCarrinhoDAO
    {
        public Banco db;
        MySqlConnection conexao = new MySqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
        MySqlCommand comand = new MySqlCommand();
        public void inserirItem(ModelItemCarrinho cm)
        {
            conexao.Open();
            MySqlCommand cmd = new MySqlCommand("insert into itenscarrinho values(default,@qt_itens,@valorParcial,@id_prod, @id_venda  ); ");



            cmd.Parameters.Add("@id_venda", MySqlDbType.VarChar).Value = cm.PedidoID;
            cmd.Parameters.Add("@qt_itens", MySqlDbType.VarChar).Value = cm.Qtd;
            cmd.Parameters.Add("@valorParcial", MySqlDbType.VarChar).Value = cm.valorParcial;
            cmd.Parameters.Add("@id_prod", MySqlDbType.VarChar).Value = cm.ProdutoID;

            cmd.Connection = conexao;
            cmd.ExecuteNonQuery();
            conexao.Close();
        }


    }
}
