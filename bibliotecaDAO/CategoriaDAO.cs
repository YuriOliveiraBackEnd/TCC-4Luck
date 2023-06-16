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
    public class CategoriaDAO
    {
        public Banco db;
        MySqlConnection conexao = new MySqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
        MySqlCommand comand = new MySqlCommand();

        public void InsertCategoria(ModelCategorias categorias)
        {
            conexao.Open();
            comand.CommandText = "call InsertCategoria(@nome_categoria, @desc_categoria);";
            comand.Parameters.Add("@nome_categoria", MySqlDbType.VarChar).Value = categorias.nome_categoria;
            comand.Parameters.Add("@desc_categoria", MySqlDbType.VarChar).Value = categorias.desc_categoria;

            comand.Connection = conexao;
            comand.ExecuteNonQuery();
            conexao.Close();
        }
        public List<ModelCategorias> Listar()
        {
            using (db = new Banco())
            {
                var strQuery = "Select * from categorias;";
                var retorno = db.Retornar(strQuery);
                return ListaDeCategoria(retorno);
            }

        }
        public string SelectNomeCategoria(string vNome)
        {
            conexao.Open();
            comand.CommandText = "call SelectNomeRaca(@nome_raca);";
            comand.Parameters.Add("@nome_raca", MySqlDbType.String).Value = vNome;
            comand.Connection = conexao;
            string nome = (string)comand.ExecuteScalar();
            conexao.Close();
            if (nome == null)
                nome = "";
            return nome;

        }
        public List<ModelCategorias> ListaDeCategoria(MySqlDataReader retorno)
        {
            var categorias = new List<ModelCategorias>();
            while (retorno.Read())
            {
                var TempCategorias = new ModelCategorias()
                {
                    id_categoria = int.Parse(retorno["id_categoria"].ToString()),
                    desc_categoria = retorno["desc_categoria"].ToString(),
                    nome_categoria = retorno["nome_categoria"].ToString(),
               
                };

                categorias.Add(TempCategorias);
            }
            retorno.Close();
            return categorias;
        }


        public ModelCategorias ListarId(int Id)
        {
            using (db = new Banco())
            {
                var strQuery = string.Format("select * from categorias where id_categoria = '{0}';", Id);
                var retorno = db.Retornar(strQuery);
                return ListaDeCategoria(retorno).FirstOrDefault();
            }
        }

        public void UpdateCategoria(ModelCategorias categorias)
        {
            var strQuery = "";
            strQuery += "Update categorias set ";
            strQuery += string.Format("nome_categoria = '{0}',", categorias.nome_categoria);
            strQuery += string.Format("desc_categoria= '{0}'", categorias.desc_categoria);
       
            strQuery += string.Format("where id_categoria = '{0}'", categorias.id_categoria);



            using (db = new Banco())
            {
                db.Executar(strQuery);
            }
        }

        public void DeleteCategoria(int id)
        {

            using (db = new Banco())
            {
                var strQuery = string.Format("Delete from Categorias where id_categoria = {0};", id);
                db.Executar(strQuery);
            }

        }
    }
}
