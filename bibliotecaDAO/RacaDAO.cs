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
    public class RacaDAO
    {
        public Banco db;
        MySqlConnection conexao = new MySqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
        MySqlCommand comand = new MySqlCommand();

        public void InsertRaca(ModelRacas racas)
        {
            conexao.Open();
            comand.CommandText = "call InsertRaca(@nome_raca,@tipo_animal, @ft_raca,@id_func);";
            comand.Parameters.Add("@nome_raca", MySqlDbType.VarChar).Value = racas.nome_raca;
            comand.Parameters.Add("@tipo_animal", MySqlDbType.VarChar).Value = racas.tipo_animal;
            comand.Parameters.Add("@ft_raca", MySqlDbType.VarChar).Value = racas.ft_raca;
            comand.Parameters.Add("@id_func", MySqlDbType.VarChar).Value = racas.id_func; 

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
        public string SelectNomeRaca(string vNome)
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
        public List<ModelRacas> Listar()
        {
            using (db = new Banco())
            {
                var strquery = ("select * from Raca;");
                var retorno = db.Retornar(strquery);
                return ListaDeRacas(retorno);

            }
        }
        public List<ModelRacas> ListaDeRacas(MySqlDataReader retorno)
        {
            var racas = new List<ModelRacas>();
            while (retorno.Read())
            {
                var TempRacas = new ModelRacas()
                {
                    id_raca = int.Parse(retorno["id_raca"].ToString()),
                    ft_raca = retorno["ft_raca"].ToString(),
                    nome_raca = retorno["nome_raca"].ToString(),
                    id_func = retorno["id_func"].ToString(),
                    tipo_animal = retorno["tipo_animal"].ToString(),

                };

                racas.Add(TempRacas);
            }
            retorno.Close();
            return racas;
        }
        public ModelRacas ListarId(int Id)
        {
            using (db = new Banco())
            {
                var strQuery = string.Format("select * from Raca where id_raca = {0};", Id);
                var retorno = db.Retornar(strQuery);
                return ListaDeRacas(retorno).FirstOrDefault();
            }
        }

        public void UpdateRaca(ModelRacas raca)
        {
            var strQuery = "";
            strQuery += "update Raca set ";
            strQuery += string.Format("nome_raca = '{0}', ft_raca = '{1}', id_func = '{2}', id_func = '{3}' where id_raca = {4};", raca.nome_raca, raca.ft_raca,raca.id_func,raca.tipo_animal, raca.id_raca);

            using (db = new Banco())
            {
                db.Executar(strQuery);
            }
        }

        public void DeleteRaca(int id)
        {

            using (db = new Banco())
            {
                var strQuery = string.Format("Delete from Raca where id_raca = {0}", id);
                db.Executar(strQuery);
            }

        }

    }
}
