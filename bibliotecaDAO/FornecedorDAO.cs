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
    public class FornecedorDAO
    {
        public Banco db;
        MySqlConnection conexao = new MySqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
        MySqlCommand comand = new MySqlCommand();

        public void InsertFornecedor(ModelFornecedor fornecedor)
        {
            conexao.Open();
            comand.CommandText = "call InsertFornecedor(@nome_forn, @tel_forn, @email_forn, @CNPJ_forn, @cep_forn, @num_forn, @logradouro_forn, @id_prod);";
            comand.Parameters.Add("@nome_forn", MySqlDbType.VarChar).Value = fornecedor.nome_forn;
            comand.Parameters.Add("@tel_forn", MySqlDbType.VarChar).Value = fornecedor.tel_forn;
            comand.Parameters.Add("@email_forn", MySqlDbType.VarChar).Value = fornecedor.email_forn;
            comand.Parameters.Add("@CNPJ_forn", MySqlDbType.VarChar).Value = fornecedor.CNPJ_forn;
            comand.Parameters.Add("@cep_forn", MySqlDbType.VarChar).Value = fornecedor.cep_forn;
            comand.Parameters.Add("@num_forn", MySqlDbType.VarChar).Value = fornecedor.num_forn;
            comand.Parameters.Add("@logradouro_forn", MySqlDbType.VarChar).Value = fornecedor.logradouro_forn;
            comand.Parameters.Add("@id_prod", MySqlDbType.Int16).Value = fornecedor.id_prod;

            comand.Connection = conexao;
            comand.ExecuteNonQuery();
            conexao.Close();
        }
        public List<ModelFornecedor> Listar()
        {
            using (db = new Banco())
            {
                var strQuery = "Select * from Fornecedor;";
                var retorno = db.Retornar(strQuery);
                return ListaDeFornecedor(retorno);
            }

        }
        public List<ModelFornecedor> ListaDeFornecedor(MySqlDataReader retorno)
        {
            var fornecedores = new List<ModelFornecedor>();
            while (retorno.Read())
            {
                var TempForn = new ModelFornecedor()
                {
                    id_forn = int.Parse(retorno["id_forn"].ToString()),
                    nome_forn = retorno["nome_forn"].ToString(),
                    email_forn = retorno["email_forn"].ToString(),
                    CNPJ_forn = retorno["CNPJ_forn"].ToString(),
                    tel_forn = retorno["tel_forn"].ToString(),
                    num_forn = int.Parse(retorno["num_forn"].ToString()),
                    cep_forn = retorno["cep_forn"].ToString(),
                    logradouro_forn = retorno["logradouro_forn"].ToString(),
                    id_prod = int.Parse(retorno["id_prod"].ToString())

                };

                fornecedores.Add(TempForn);
            }
            retorno.Close();
            return fornecedores;
        }


        public ModelFornecedor ListarId(int Id)
        {
            using (db = new Banco())
            {
                var strQuery = string.Format("select * from Fornecedor where id_forn = {0};", Id);
                var retorno = db.Retornar(strQuery);
                return ListaDeFornecedor(retorno).FirstOrDefault();
            }
        }

        public void Updatefornecedor(ModelFornecedor fornecedor)
        {
            var strQuery = "";
            strQuery += "update Fornecedor set ";
            strQuery += string.Format("nome_forn = '{0}', email_forn = '{1}', CNPJ_forn = '{2}', cep_forn = '{3}', num_forn = '{4}', logradouro_forn = '{5}', tel_forn = '{6}' where id_forn = {7};", fornecedor.nome_forn, fornecedor.email_forn, fornecedor.CNPJ_forn, fornecedor.cep_forn, fornecedor.num_forn, fornecedor.logradouro_forn, fornecedor.tel_forn, fornecedor.id_forn);

            using (db = new Banco())
            {
                db.Executar(strQuery);
            }
        }

        public void DeleteForn(int id)
        {

            using (db = new Banco())
            {
                var strQuery = string.Format("Delete from Fornecedor where id_forn = {0};", id);
                db.Executar(strQuery);
            }

        }

        public void Save(ModelFornecedor fornecedor)
        {
            if (fornecedor.id_forn > 0)
            {
                Updatefornecedor(fornecedor);
            }
            else
            {
                InsertFornecedor(fornecedor);
            }
        }
        public string SelectEmailForn(string vEmail)
        {
            conexao.Open();
            comand.CommandText = "call  SelectEmailDoForn(@email_forn);";
            comand.Parameters.Add("@email_forn", MySqlDbType.String).Value = vEmail;
            comand.Connection = conexao;
            string Email = (string)comand.ExecuteScalar();
            conexao.Close();
            if (Email == null)
                Email = "";
            return Email;
        }
        public string SelectCNPJFonc(string vCNPJ)
        {
            conexao.Open();
            comand.CommandText = "call SelectCNPJDoForn(@CNPJ_forn);";
            comand.Parameters.Add("@CNPJ_forn", MySqlDbType.VarChar).Value = vCNPJ;
            comand.Connection = conexao;
            string CNPJ = (string)comand.ExecuteScalar();
            conexao.Close();
            if (CNPJ == null)



                CNPJ = "";
            return CNPJ;

        }

    }
}
