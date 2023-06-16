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
    public class FuncionarioDAO
    {
        public Banco db;
        MySqlConnection conexao = new MySqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
        MySqlCommand comand = new MySqlCommand();



        public void InsertFuncionario(ModelFuncionario funcionario)
        {
            conexao.Open();
            comand.CommandText = "call InsertFuncionario(@nome_func,@email_func, @CPF_func, @cep_func, @num_func, @logradouro_func, @nasc_func, @tel_func,@senha_func);";
            comand.Parameters.Add("@nome_func", MySqlDbType.VarChar).Value = funcionario.nome_func;
            comand.Parameters.Add("@email_func", MySqlDbType.VarChar).Value = funcionario.email_func;
            comand.Parameters.Add("@CPF_func", MySqlDbType.VarChar).Value = funcionario.CPF_func;
            comand.Parameters.Add("@cep_func", MySqlDbType.VarChar).Value = funcionario.cep_func;
            comand.Parameters.Add("@num_func", MySqlDbType.VarChar).Value = funcionario.num_func;
            comand.Parameters.Add("@logradouro_func", MySqlDbType.VarChar).Value = funcionario.logradouro_func;
            comand.Parameters.Add("@nasc_func", MySqlDbType.VarChar).Value = funcionario.nasc_func;
            comand.Parameters.Add("@tel_func", MySqlDbType.VarChar).Value = funcionario.tel_func;
            comand.Parameters.Add("@senha_func", MySqlDbType.VarChar).Value = funcionario.senha_func;
            comand.Connection = conexao;
            comand.ExecuteNonQuery();
            conexao.Close();
        }

        public string SelectEmailFunc(string vEmail)
        {
            conexao.Open();
            comand.CommandText = "call SelectEmailDoFunc(@email_func);";
            comand.Parameters.Add("@email_func", MySqlDbType.String).Value = vEmail;
            comand.Connection = conexao;
            string Email = (string)comand.ExecuteScalar();
            conexao.Close();
            if (Email == null)
                Email = "";
            return Email;
        }
        public string SelectCPFFunc(string vCPF)
        {
            conexao.Open();
            comand.CommandText = "call SelectCPFDoFunc(@CPF_func);";
            comand.Parameters.Add("@CPF_func", MySqlDbType.VarChar).Value = vCPF;
            comand.Connection = conexao;
            string CPF = (string)comand.ExecuteScalar();
            conexao.Close();
            if (CPF == null)



                CPF = "";
            return CPF;

        }

        public ModelFuncionario SelectFuncionario(string vEmail)
        {
            conexao.Open();
            comand.CommandText = "call SelectFuncionario(@email_func);";
            comand.Parameters.Add("@email_func", MySqlDbType.VarChar).Value = vEmail;



            comand.Connection = conexao;
            var readFunc = comand.ExecuteReader();
            var tempFunc = new ModelFuncionario();



            if (readFunc.Read())
            {
                tempFunc.id_func = int.Parse(readFunc["id_func"].ToString());
                tempFunc.nome_func = readFunc["nome_func"].ToString();
                tempFunc.num_func = readFunc["num_func"].ToString();
                tempFunc.cep_func = readFunc["cep_func"].ToString();
                tempFunc.tel_func = readFunc["tel_func"].ToString();
                tempFunc.email_func = readFunc["email_func"].ToString();
                tempFunc.nasc_func = readFunc["nasc_func"].ToString();
                tempFunc.logradouro_func = readFunc["logradouro_func"].ToString();
                tempFunc.senha_func = readFunc["senha_func"].ToString();
                tempFunc.CPF_func = readFunc["CPF_func"].ToString();

            }
            readFunc.Close();
            conexao.Close();
            return tempFunc;
        }
        public List<ModelFuncionario> Listar()
        {
            using (db = new Banco())
            {
                var strQuery = "Select * from Funcionario;";
                var retorno = db.Retornar(strQuery);
                return ListaDeFuncionarios(retorno);
            }
        }
        public List<ModelFuncionario> ListaDeFuncionarios(MySqlDataReader retorno)
        {
            var funcionarios = new List<ModelFuncionario>();
            while (retorno.Read())
            {
                var TempFunc = new ModelFuncionario()
                {
                    id_func = int.Parse(retorno["id_func"].ToString()),
                    nome_func = retorno["nome_func"].ToString(),
                    email_func = retorno["email_func"].ToString(),
                    CPF_func = retorno["CPF_func"].ToString(),
                    tel_func = retorno["tel_func"].ToString(),
                    senha_func = retorno["senha_func"].ToString(),
                    num_func = retorno["num_func"].ToString(),
                    cep_func = retorno["cep_func"].ToString(),
                    logradouro_func = retorno["logradouro_func"].ToString(),
                    nasc_func = retorno["nasc_func"].ToString()

                };


                funcionarios.Add(TempFunc);
            }
            retorno.Close();
            return funcionarios;
        }


        public ModelFuncionario ListarId(int Id)
        {
            using (db = new Banco())
            {
                var strQuery = string.Format("select * from Funcionario where id_func = '{0}';", Id);
                var retorno = db.Retornar(strQuery);
                return ListaDeFuncionarios(retorno).FirstOrDefault();
            }
        }

        public ModelFuncionario ListarEmail(string Login)
        {
            using (db = new Banco())
            {
                var strQuery = string.Format("select * from Funcionario where email_func = '{0}';", Login);
                var retorno = db.Retornar(strQuery);
                return ListaDeFuncionarios(retorno).FirstOrDefault();
            }
        }


        public void UpdateFuncionario(ModelFuncionario funcionario)
        {
            var strQuery = "";
            strQuery += "Update funcionario set ";
            strQuery += string.Format("nome_func = '{0}',", funcionario.nome_func);
            strQuery += string.Format("email_func= '{0}', ", funcionario.email_func);
            strQuery += string.Format("CPF_func = '{0}',", funcionario.CPF_func);
            strQuery += string.Format("tel_func = '{0}',", funcionario.tel_func);
            strQuery += string.Format("num_func = '{0}',", funcionario.num_func);
            strQuery += string.Format("senha_func = '{0}',", funcionario.senha_func);
            strQuery += string.Format("cep_func = '{0}',", funcionario.cep_func);
            strQuery += string.Format("logradouro_func = '{0}',", funcionario.logradouro_func);
            strQuery += string.Format("nasc_func = '{0}'", funcionario.nasc_func);

            strQuery += string.Format("where id_func = '{0}'", funcionario.id_func);



            using (db = new Banco())
            {
                db.Executar(strQuery);
            }
        
    }
        public void DeleteFunc(int id)
        {

            using (db = new Banco())
            {
                var strQuery = string.Format("Delete from Funcionario where id_func = {0};", id);
                db.Executar(strQuery);
            }

        }


        public void Save(ModelFuncionario funcionario)
        {
            if (funcionario.id_func > 0)
            {
                UpdateFuncionario(funcionario);
            }
            else
            {
                InsertFuncionario(funcionario);
            }
        }
    }
}