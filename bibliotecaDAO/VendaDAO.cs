using bibliotecaBanco;
using bibliotecaModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace bibliotecaDAO
{
    public class VendaDAO
    {
        public Banco db;
        MySqlConnection conexao = new MySqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
        MySqlCommand comand = new MySqlCommand();

        public void InsertVenda(ModelVenda venda)
        {
            conexao.Open();
            comand.CommandText = "call InsertVenda(@id_cli,@pagamento,@datavenda,@horaVenda, @valorFinal) ;";
            comand.Parameters.Add("@pagamento", MySqlDbType.VarChar).Value = venda.pagamento;
            comand.Parameters.Add("@datavenda", MySqlDbType.VarChar).Value = venda.data_venda;
            comand.Parameters.Add("@id_cli", MySqlDbType.VarChar).Value = venda.id_cli;
            comand.Parameters.Add("@horaVenda", MySqlDbType.VarChar).Value = venda.horaVenda;
            comand.Parameters.Add("@valorFinal", MySqlDbType.VarChar).Value = venda.ValorTotal;


            comand.Connection = conexao;
            comand.ExecuteNonQuery();
            conexao.Close();

        
        }


        public List<ModelVenda> Listar()
        {
            using (db = new Banco())
            {
                var strquery = ("select * from vendas;");
                var retorno = db.Retornar(strquery);
                return ListaDeVendas(retorno);

            }
        }
        public List<ModelVenda> ListaDeVendas(MySqlDataReader retorno)
        {
            var vendas = new List<ModelVenda>();
            while (retorno.Read())
            {
                var TempVendas = new ModelVenda()
                {
                    id_venda = retorno["id_venda"].ToString(),
                    pagamento= retorno["pagamento"].ToString(),
                    id_cli = retorno["id_cli"].ToString(),
                    data_venda = retorno["data_venda"].ToString(),
                    horaVenda = retorno["horaVenda"].ToString(),
                    ValorTotal = int.Parse(retorno["valorFinal"].ToString()),

                };

                vendas.Add(TempVendas);
            }
            retorno.Close();
            return vendas;
        }

  
        MySqlDataReader dr;
        public void buscaIdVenda(ModelVenda vend)
        {
            conexao.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT id_venda FROM venda ORDER BY id_venda DESC limit 1",conexao);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                vend.id_venda = dr[0].ToString();
            }
            conexao.Close(); ;
        }
      



    }
}
