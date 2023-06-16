using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using bibliotecaModel;
using bibliotecaBanco;



namespace bibliotecaDAO
{
    public class ClienteDAO
    {



        public Banco db;



        MySqlConnection conexao = new MySqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
        MySqlCommand comand = new MySqlCommand();


        //insere o cliente
        public void InsertCliente(ModelCliente cliente)
        {
            conexao.Open();
            comand.CommandText = "call InsertCliente(@nome_cli, @tel_cli, @email_cli, @CPF_cli, @cep_cli, @num_cli, @logradouro_cli, @nasc_cli,@senha_cli);";
            comand.Parameters.Add("@nome_cli", MySqlDbType.VarChar).Value = cliente.nome_cli;
            comand.Parameters.Add("@tel_cli", MySqlDbType.VarChar).Value = cliente.tel_cli;
            comand.Parameters.Add("@email_cli", MySqlDbType.VarChar).Value = cliente.email_cli;
            comand.Parameters.Add("@CPF_cli", MySqlDbType.VarChar).Value = cliente.CPF_cli;
            comand.Parameters.Add("@cep_cli", MySqlDbType.VarChar).Value = cliente.cep_cli;
            comand.Parameters.Add("@num_cli", MySqlDbType.VarChar).Value = cliente.num_cli;
            comand.Parameters.Add("@logradouro_cli", MySqlDbType.VarChar).Value = cliente.logradouro_cli;
            comand.Parameters.Add("@nasc_cli", MySqlDbType.VarChar).Value = cliente.nasc_cli;
            comand.Parameters.Add("@senha_cli", MySqlDbType.VarChar).Value = cliente.senha_cli;

            comand.Connection = conexao;
            comand.ExecuteNonQuery();
            conexao.Close();
        }

        public string SelectEmailDoCliente(string vEmail)
        {
            conexao.Open();
            comand.CommandText = "call SelectEmailDoCliente(@email_cli);";
            comand.Parameters.Add("@email_cli", MySqlDbType.VarChar).Value = vEmail;
            comand.Connection = conexao;
            string Email = (string)comand.ExecuteScalar();
            conexao.Close();
            if (Email == null)



                Email = "";
            return Email;
        }


        public ModelCliente SelectCliente(string vEmail)
        {
            conexao.Open();
            comand.CommandText = "call SelectCliente(@email_cli);";
            comand.Parameters.Add("@email_cli", MySqlDbType.VarChar).Value = vEmail;



            comand.Connection = conexao;
            var readcli = comand.ExecuteReader();
            var tempcli = new ModelCliente();



            if (readcli.Read())
            {
                tempcli.id_cli = int.Parse(readcli["id_cli"].ToString());
                tempcli.nome_cli = readcli["nome_cli"].ToString();
                tempcli.num_cli = readcli["num_cli"].ToString();
                tempcli.cep_cli = readcli["cep_cli"].ToString();
                tempcli.tel_cli = readcli["tel_cli"].ToString();
                tempcli.email_cli = readcli["email_cli"].ToString();
                tempcli.nasc_cli = readcli["nasc_cli"].ToString();
                tempcli.logradouro_cli = readcli["logradouro_cli"].ToString();
                tempcli.senha_cli = readcli["senha_cli"].ToString();
                tempcli.CPF_cli = readcli["CPF_cli"].ToString();

            }
            readcli.Close();
            conexao.Close();
            return tempcli;
        }
        public string SelectCPFDoCliente(string vCPF)
        {
            conexao.Open();
            comand.CommandText = "call SelectCPFDoCliente(@CPF_cli);";
            comand.Parameters.Add("@CPF_cli", MySqlDbType.String).Value = vCPF;
            comand.Connection = conexao;
            string CPF = (string)comand.ExecuteScalar();
            conexao.Close();
            if (CPF == null)



                CPF = "";
            return CPF;

        }


        //cria uma lista com os dados do cliente
        public List<ModelCliente> ListaDeClientes(MySqlDataReader retorno)
        {
            var clientes = new List<ModelCliente>();
            while (retorno.Read())
            {
                var TempCliente = new ModelCliente()
                {
                    id_cli = int.Parse(retorno["id_cli"].ToString()),
                    nome_cli = retorno["nome_cli"].ToString(),
                    email_cli = retorno["email_cli"].ToString(),
                    CPF_cli = retorno["CPF_cli"].ToString(),
                    tel_cli = retorno["tel_cli"].ToString(),
                    num_cli = retorno["num_cli"].ToString(),
                    cep_cli = retorno["cep_cli"].ToString(),
                    logradouro_cli = retorno["logradouro_cli"].ToString(),
                    nasc_cli = retorno["nasc_cli"].ToString(),
                    senha_cli = retorno["senha_cli"].ToString()
                };
                clientes.Add(TempCliente);
            }

            retorno.Close();
            return clientes;
        }
        // lista um cliente pelo email
        public ModelCliente ListarId(string Login)
        {
            using (db = new Banco())
            {
                var db = new Banco();
                var strQuery = string.Format("select * from Cliente where email_cli = '{0}';", Login);
                var retorno = db.Retornar(strQuery);
                return ListaDeClientes(retorno).FirstOrDefault();
            }
        }

        public ModelCliente ListarporID(int id)
        {
            using (db = new Banco())
            {
                var db = new Banco();
                var strQuery = string.Format("select * from Cliente where id_cli = '{0}';", id);
                var retorno = db.Retornar(strQuery);
                return ListaDeClientes(retorno).FirstOrDefault();
            }
        }
        //seleciona todos os clientes cadastrados
        public List<ModelCliente> Listar()
        {
            using (db = new Banco())
            {
                var strQuery = "Select * from Cliente;";
                var retorno = db.Retornar(strQuery);
                return ListaDeClientes(retorno);
            }
        }




        //cria uma lista com os dados do cliente e dos pets dele
        public List<ModelCliente> ListaDeClientesePet(MySqlDataReader retorno)
        {
            var clientes = new List<ModelCliente>();
            while (retorno.Read())
            {
                var TempCliente = new ModelCliente()
                {
                    nome_cli = retorno["cliente"].ToString(),
                    email_cli = retorno["email"].ToString(),
                    CPF_cli = retorno["CPF"].ToString(),
                    tel_cli = retorno["telefone"].ToString(),
                    num_cli = retorno["numero"].ToString(),
                    cep_cli = retorno["CEP"].ToString(),
                    logradouro_cli = retorno["rua"].ToString(),
                    nasc_cli = retorno["nascimento"].ToString(),
                    senha_cli = retorno["senha"].ToString()               
                };
                clientes.Add(TempCliente);
            }

            retorno.Close();
            return clientes;
        }
        // lista um cliente e seus pets pelo email
        public ModelCliente ListarEmail(string Login)
        {
            using (db = new Banco())
            {
                var db = new Banco();
                var strQuery = string.Format("select c.nome_cli as cliente, c.tel_cli as telefone, c.email_cli as email, c.CPF_cli as CPF, c.cep_cli as CEP, c.num_cli as numero, " +
                    "c.logradouro_cli as rua, c.nasc_cli as nascimento, c.senha_cli as senha " +
                    "from db4luck.Cliente c, db4luck.Pets p " +
                    "where c.id_cli = p.id_cli and c.email_cli = '{0}';", Login);
                var retorno = db.Retornar(strQuery);
                return ListaDeClientesePet(retorno).FirstOrDefault();
            }
        }


        // cria uma lista com os dados do pet
        public List<ModelCliente> ListaDePet(MySqlDataReader retorno)
        {
            var pet = new List<ModelCliente>();
            while (retorno.Read())
            {
                var TempPet = new ModelCliente()
                {
                    id_pet = int.Parse(retorno["id_pet"].ToString()),
                    nome_pet = retorno["nome_pet"].ToString(),
                    RGA = retorno["RGA"].ToString(),
                    nasc_pet = retorno["nasc_pet"].ToString()
                };
                pet.Add(TempPet);
            }
            retorno.Close();
            return pet;
        }
        // lista os pets pelo id
        public ModelCliente ListarIdPet(int Id)
        {
            using (db = new Banco())
            {
                var strQuery = string.Format("select * from Pets where id_pet = {0};", Id);
                var retorno = db.Retornar(strQuery);
                return ListaDePet(retorno).FirstOrDefault();
            }
        }


        //atualiza o cliente
        public void UpdateCliente(ModelCliente cliente)
        {
            var strQuery = "";
            strQuery += "Update cliente set ";
            strQuery += string.Format("nome_cli = '{0}',", cliente.nome_cli);
            strQuery += string.Format("email_cli= '{0}', ", cliente.email_cli);
            strQuery += string.Format("CPF_cli = '{0}',", cliente.CPF_cli);
            strQuery += string.Format("tel_cli = '{0}',", cliente.tel_cli);
            strQuery += string.Format("num_cli = '{0}',", cliente.num_cli);
            strQuery += string.Format("cep_cli = '{0}',", cliente.cep_cli);
            strQuery += string.Format("logradouro_cli = '{0}',", cliente.logradouro_cli);
            strQuery += string.Format("nasc_cli = '{0}' ", cliente.nasc_cli);
          
            strQuery += string.Format("where id_cli = '{0}'", cliente.id_cli);

            using (db = new Banco())
            {
                db.Executar(strQuery);
            }
        }

        //atualiza os dados do pet do cliente
        public void UpdatePet(ModelCliente pets)
        {
            var strQuery = "";
            strQuery += "update Pets set ";
            strQuery += string.Format("nome_pet = '{0}',", pets.nome_pet);
            strQuery += string.Format("ft_pet = '{0}',", pets.ft_pet);
            strQuery += string.Format("nasc_pet= '{0}',", pets.nasc_pet);
            strQuery += string.Format(" RGA = '{0}',", pets.RGA);
            strQuery += string.Format("id_raca = '{0}',", pets.id_raca);
            strQuery += string.Format("id_cli = '{0}' ", pets.id_cli);
            strQuery += string.Format("where id_pet = '{0}'", pets.id_pet);

            using (db = new Banco())
            {
                db.Executar(strQuery);
            }

        }



        public void Excluir(int id)
        {

            using (db = new Banco())
            {
                var strQuery = string.Format("Delete from cliente where id_cli = {0}", id);
                db.Executar(strQuery);
            }

        }

    }
}