using bibliotecaBanco;
using bibliotecaModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace bibliotecaDAO
{
    public class ProdutoDAO
    {
        public Banco db;
        MySqlConnection conexao = new MySqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
        MySqlCommand comand = new MySqlCommand();
      

 
        public void InsertProduto(ModelProduto produto)
        {
            conexao.Open();
            comand.CommandText = "call InsertProduto(@nome_prod,@valor_unitario, @quant, @desc_prod, @ft_prod, @id_func,@id_categoria);";
            comand.Parameters.Add("@valor_unitario", MySqlDbType.Double).Value = produto.valor_unitario;
            comand.Parameters.Add("@nome_prod", MySqlDbType.VarChar).Value = produto.nome_prod;
            comand.Parameters.Add("@quant", MySqlDbType.VarChar).Value = produto.quant;
            comand.Parameters.Add("@desc_prod", MySqlDbType.VarChar).Value = produto.desc_prod;
            comand.Parameters.Add("@ft_prod", MySqlDbType.VarChar).Value = produto.ft_prod;
            comand.Parameters.Add("@id_func", MySqlDbType.VarChar).Value = produto.id_func;
            comand.Parameters.Add("@id_categoria", MySqlDbType.VarChar).Value = produto.id_categoria;
         

            comand.Connection = conexao;
            comand.ExecuteNonQuery();
            conexao.Close();
        }

        public string SelectIdDofunc(string Email)
        {
            string VEmail = "";
            conexao.Open();
            comand.CommandText = "call SelectIdfunc(@email_func);";
            comand.Parameters.Add("@email_func", MySqlDbType.VarChar).Value = Email;
            comand.Connection = conexao;
            object result = comand.ExecuteScalar();
            if (result != null)
            {
                int id = (int)result; // Converte o resultado para int
                VEmail = id.ToString();
            }
            return VEmail;
        }

        public List<ModelProduto> Listar()
        {
            using (db = new Banco())
            {
                var strQuery = "Select * from Produto;";
                var retorno = db.Retornar(strQuery);
                return ListaDeProduto(retorno);
            }

        }
        public List<ModelProduto> ListarPlanos()
        {
            using (db = new Banco())
            {
                var strQuery = "SELECT Produto.*, Categorias.nome_categoria FROM Produto JOIN Categorias ON Produto.id_categoria = Categorias.id_categoria WHERE Categorias.nome_categoria LIKE '%plano%'; ";
                var retorno = db.Retornar(strQuery);
                return ListaDeProduto(retorno);
            }

        }
        public List<ModelProduto> ListarProdCat()
        {
            using (db = new Banco())
            {
                var strQuery = "SELECT Produto.*, Categorias.nome_categoria FROM Produto JOIN Categorias ON Produto.id_categoria = Categorias.id_categoria WHERE Categorias.nome_categoria LIKE '%gato%'; ";
                var retorno = db.Retornar(strQuery);
                return ListaDeProduto(retorno);
            }

        }
        public List<ModelProduto> ListarProdDog()
        {
            using (db = new Banco())
            {
                var strQuery = "SELECT Produto.*, Categorias.nome_categoria FROM Produto JOIN Categorias ON Produto.id_categoria = Categorias.id_categoria WHERE Categorias.nome_categoria LIKE '%cachorro%'; ";
                var retorno = db.Retornar(strQuery);
                return ListaDeProduto(retorno);
            }

        }
   
        public List<ModelProduto> ListaDeProduto(MySqlDataReader retorno)
        {
            var produtos = new List<ModelProduto>();
            while (retorno.Read())
            {
                var TempProd = new ModelProduto()
                {
                    id_prod = int.Parse(retorno["id_prod"].ToString()),
                    nome_prod = retorno["nome_prod"].ToString(),
                    id_categoria = int.Parse(retorno["id_categoria"].ToString()),
                    quant = int.Parse(retorno["quant"].ToString()),
                    valor_unitario = double.Parse(retorno["valor_unitario"].ToString()),
                    desc_prod = retorno["desc_prod"].ToString(),
                    ft_prod = retorno["ft_prod"].ToString()

                };

                produtos.Add(TempProd);
            }
            retorno.Close();
            return produtos;
        }
        public List<ModelProduto> GetConsProd(int id)
        {
            conexao.Open();
            List<ModelProduto> Produtoslist = new List<ModelProduto>(); 
         
            MySqlCommand cmd = new MySqlCommand("select * from produto where id_prod=@cod",conexao);
            cmd.Parameters.AddWithValue("@cod", id);
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            conexao.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Produtoslist.Add(
                    new ModelProduto
                    {
                        id_prod = Convert.ToInt32(dr["id_prod"]),
                        nome_prod = Convert.ToString(dr["nome_prod"]),
                        id_categoria = Convert.ToInt32(dr["id_categoria"]),
                        quant = Convert.ToInt32(dr["quant"]),
                        valor_unitario = Convert.ToDouble(dr["valor_unitario"]),
                        desc_prod = Convert.ToString(dr["desc_prod"]),
                        ft_prod = Convert.ToString(dr["ft_prod"])
                    });
            }
            return Produtoslist;
        }
        public List<ModelProduto> Pesquisa(string pesquisar)
        {
            using (db = new Banco())
            {
                var strQuery = string.Format("select * from Produto where nome_prod like '%{0}%';", pesquisar);
                var retorno = db.Retornar(strQuery);
                return ListaDeProduto(retorno);
            }
        }

        public ModelProduto ListarId(int Id)
        {
            using (db = new Banco())
            {
                var strQuery = string.Format("select * from Produto where id_prod = '{0}';", Id);
                var retorno = db.Retornar(strQuery);
                return ListaDeProduto(retorno).FirstOrDefault();
            }
        }

        public void UpdateProduto(ModelProduto produto)
        {
            conexao.Open();
            MySqlCommand cmd = new MySqlCommand("update produto set valor_unitario=@valor_unitario, nome_prod=@nome_prod, quant=@quant,desc_prod=@desc_prod,id_categoria=@id_categoria,ft_prod=@ft_prod,id_func=@id_func  WHERE id_prod =@id_prod");


            cmd.Parameters.AddWithValue("@valor_unitario", produto.valor_unitario);

            cmd.Parameters.AddWithValue("@nome_prod", produto.nome_prod);

            cmd.Parameters.AddWithValue("@quant", produto.quant);

            cmd.Parameters.AddWithValue("@desc_prod", produto.desc_prod);

            cmd.Parameters.AddWithValue("@id_categoria", produto.id_categoria);

            cmd.Parameters.AddWithValue("@ft_prod", produto.ft_prod);

            cmd.Parameters.AddWithValue("@id_func", produto.id_func);

            cmd.Parameters.AddWithValue("@id_prod", produto.id_prod);

            cmd.Connection = conexao;
            cmd.ExecuteNonQuery();
            conexao.Close();

        }

        public void DeleteProd(int id)
        {

            using (db = new Banco())
            {
                var strQuery = string.Format("Delete from Produto where id_prod = {0};", id);
                db.Executar(strQuery);
            }

        }

    }
}
