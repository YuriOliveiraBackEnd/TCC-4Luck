using bibliotecaDAO;
using bibliotecaModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace PlanosPets.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente

        //metodo para carregar as raças cadastradas no banco
        public void CarregaRacaCachorro( )
        {
            List<SelectListItem> cachorro = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=db4luck;User=root;pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Raca where tipo_animal = 'cachorro'", con);
                MySqlDataReader rdr = cmd.ExecuteReader();



                while (rdr.Read())
                {
                    cachorro.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
            }
            ViewBag.cachorro = new SelectList(cachorro, "Value", "Text");
        }
        public void CarregaRacaGato()
        {
            List<SelectListItem> gato = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=db4luck;User=root;pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Raca where tipo_animal = 'gato'", con);
                MySqlDataReader rdr = cmd.ExecuteReader();



                while (rdr.Read())
                {
                    gato.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
            }
            ViewBag.gato = new SelectList(gato, "Value", "Text");
        }


        public ActionResult Index()
        {
            if (Session["FuncLogado"] == null)
            {
                return RedirectToAction("SemAcesso", "Login");
            }
            else
            {
                var metodoCliente = new ClienteDAO();
                var listaCliente = metodoCliente.Listar();
                return View(listaCliente);
            }
        }

        //tela de cadastro do cliente
        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }
        //action com todas as validações de cadastro
        [HttpPost]
        public ActionResult Cadastrar(ModelCliente cliente)
        {
            ModelState.Remove("RGA");
            ModelState.Remove("nome_pet");
            ModelState.Remove("nasc_pet");
            if (!ModelState.IsValid)

            {
                return View(cliente);
            }

                ClienteDAO novoClienteDAO = new ClienteDAO();
                string cpf = new ClienteDAO().SelectCPFDoCliente(cliente.CPF_cli);
                string email = new ClienteDAO().SelectEmailDoCliente(cliente.email_cli);
                if (cpf == cliente.CPF_cli && email == cliente.email_cli)
                {
                    ViewBag.Email = "Email já existente";
                    ViewBag.CPF = "CPF já existente";
                    return View(cliente);
                }

                else if (cpf == cliente.CPF_cli)
                {
                    ViewBag.CPF = "CPF já existente";
                    return View(cliente);
                }

                else if (email == cliente.email_cli)
                {
                    ViewBag.Email = "Email já existente";
                    return View(cliente);
                };
                ModelCliente novoCliente = new ModelCliente()
                {
                    nome_cli = cliente.nome_cli,
                    email_cli = cliente.email_cli,
                    CPF_cli = cliente.CPF_cli,
                    cep_cli = cliente.cep_cli,
                    num_cli = cliente.num_cli,
                    logradouro_cli = cliente.logradouro_cli,
                    nasc_cli = cliente.nasc_cli,
                    tel_cli = cliente.tel_cli,
                    senha_cli = cliente.senha_cli
                };
                novoClienteDAO.InsertCliente(novoCliente);


                FormsAuthentication.SetAuthCookie(cliente.email_cli, false);
                Session["ClienteLogado"] = cliente.email_cli.ToString();
                Session["senhaLogado"] = cliente.senha_cli.ToString();

                return RedirectToAction("Index", "Home");
            }
          
        
          
        

        //action que lista o cliente
        //seleciona o cliente que está logado através da session
        public ActionResult Atualizar()
        {
            string Login = Session["ClienteLogado"] as string;

            var metodoCliente = new ClienteDAO();
            var cliente = metodoCliente.ListarId(Login);


            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
            
        }
        //action que aciona o método de atualizar
        [HttpPost]
        public ActionResult Atualizar(ModelCliente cliente)
        {
            if (!ModelState.IsValid)
            {
                var metodoCliente = new ClienteDAO();
                metodoCliente.UpdateCliente(cliente);
                return RedirectToAction("DetalhesCliPet", "Cliente");
            }
            return View(cliente);
        }


        public ActionResult Excluir(int id)

        {
            var metodoCliente = new ClienteDAO();
            metodoCliente.Excluir(id);

            return RedirectToAction("Index");

        }

       

        //action que mostra o perfil com os dados do cliente logado, junto com seus pets cadastrados
        public ActionResult DetalhesCliPet()
        {
            string Login = Session["ClienteLogado"] as string;

            var metodoCliente = new ClienteDAO();
            var cliente = metodoCliente.ListarEmail(Login);
            if (cliente == null)
            {
                return RedirectToAction("Detalhes");
            }
            return View(cliente);
        }
        //action que mostra o perfil com os dados do cliente logado
        public ActionResult Detalhes()
        {
            string Login = Session["ClienteLogado"] as string;

            var metodoCliente = new ClienteDAO();
            var cliente = metodoCliente.ListarId(Login);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        public ActionResult DetalhesdoFunc(int id)
        {
            var metodoCliente = new ClienteDAO();
            var cliente = metodoCliente.ListarporID(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        public ActionResult ListarPet()
        {
            string Login = Session["ClienteLogado"] as string;

            var metodoPet = new PetDAO();
            var listaPet = metodoPet.ListarPetCli(Login);
            return View(listaPet);

        }

        //action que lista o pet pelo id
        public ActionResult AtualizarPet(int Id)
        {
            var metodopet = new PetDAO();
            var pet = metodopet.ListarIdPet(Id);

            if(pet == null)
            {
                return HttpNotFound();
            }
            CarregaRacaCachorro();
            CarregaRacaGato();
            return View(pet);
        }
        //action que aciona o método de atualizar
        [HttpPost]
        public ActionResult AtualizarPet(ModelCliente pets,int id)
        {
            pets.id_gato = Request["gato"];
            if (pets.id_gato == "")
            {
                pets.id_cachorro = Request["cachorro"];
                if (pets.id_cachorro != "")
                {
                    pets.id_raca = int.Parse(pets.id_cachorro);
                }
            }
            else
            {
                pets.id_raca = int.Parse(pets.id_gato);
            }
            string Email = Session["ClienteLogado"] as string;
            string idCli = new PetDAO().SelectIdDoCli(Email);
            pets.id_cli = int.Parse(idCli);
            var metodoUsuario = new ClienteDAO();
            pets.id_raca = pets.id_raca;
            pets.id_pet = id;
            metodoUsuario.UpdatePet(pets);
            return RedirectToAction("ListarPet");

        }

        //action para excluir um pet, retornando para a lista caso o cliente ainda tenha pets cadastrados
        //ou retornando para o perfil do cliente, caso não tenha mais
        public ActionResult ExcluirPet(int id)
        {
            string Login = Session["ClienteLogado"] as string;

            var metodopet = new PetDAO();
            metodopet.DeletePet(id);
            var metodoCliente = new ClienteDAO();
            var cliente = metodoCliente.ListarEmail(Login);

            if (cliente == null)
            {
                return RedirectToAction("Detalhes");
            }
            else
                return RedirectToAction("ListarPet");
        }
    }
}