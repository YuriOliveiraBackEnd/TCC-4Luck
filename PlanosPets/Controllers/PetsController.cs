using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using System.Configuration;
using MySql.Data.MySqlClient;
using bibliotecaDAO;
using bibliotecaModel;
using System.IO;
using System.Web.UI.WebControls;

namespace PlanosPets.Controllers
{
    public class PetsController : Controller
    {
        // GET: Pets
        public void CarregaRacaCachorro()
        {
            List<SelectListItem> cachorro = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=db4luck;User=root;pwd=metranca789456123"))
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

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=db4luck;User=root;pwd=metranca789456123"))
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
                var metodoPet = new PetDAO();
                var listaPet = metodoPet.Listar();
                return View(listaPet);
            }

        }
        public ActionResult Excluir(int id)
        {
            if (Session["FuncLogado"] == null)
            {
                return RedirectToAction("SemAcesso", "Login");
            }
            else
            {
                var metodoPet = new PetDAO();
                var pet = metodoPet.ListarIdPet(id);
                if (pet == null)
                {
                    return HttpNotFound();
                }
                return View(pet);
            }
        }
        [HttpPost, ActionName("Excluir")]
        public ActionResult ExcluirConfirma(int id)
        {
            var metodoPet = new PetDAO();
            ModelCliente pet = new ModelCliente();
            pet.id_pet = id;
            metodoPet.DeletePet(id);
            return RedirectToAction("Index");

        }
        public ActionResult ListarPet()
        {
            string Login = Session["ClienteLogado"] as string;

            var metodoPet = new PetDAO();
            var listaPet = metodoPet.ListarPetCli(Login);
            return View(listaPet);

        }

        public ActionResult Cadastrar()
        {
            if (Session["ClienteLogado"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                CarregaRacaCachorro();
                CarregaRacaGato();
                return View();
            }
        }
        [HttpPost]
        public ActionResult Cadastrar(ModelCliente pets, HttpPostedFileBase file)
        {
          
                   
            
                 
            ModelState.Remove("nome_cli");
            ModelState.Remove("nasc_cli");
            ModelState.Remove("email_cli");
            ModelState.Remove("CPF_cli");
            ModelState.Remove("cep_cli");
            ModelState.Remove("num_cli");
            ModelState.Remove("logradouro_cli");
            ModelState.Remove("tel_cli");
            ModelState.Remove("senha_cli");
            ModelState.Remove("cidade");
            ModelState.Remove("uf");
            ModelState.Remove("bairro");
            ModelState.Remove("Confirmar_senha");
            if (!ModelState.IsValid)
            {
                CarregaRacaCachorro();
                CarregaRacaGato();
                return View(pets);
            }
            PetDAO novapetsDAO = new PetDAO();
            string RGA = new PetDAO().SelectRGA(pets.RGA);
            if (RGA ==pets.RGA )
            {
                CarregaRacaCachorro();
                CarregaRacaGato();
                ViewBag.RGA = "RGA já cadastrado";
                return View(pets);
            }
            string Email = Session["ClienteLogado"] as string;
            string id = new PetDAO().SelectIdDoCli(Email);
            pets.id_cli = int.Parse(id);


            string arquivo = Path.GetFileName(file.FileName);

            string file2 = "/images/" + Path.GetFileName(file.FileName);

            string _path = Path.Combine(Server.MapPath("~/images"), arquivo);

            file.SaveAs(_path);

            pets.ft_pet = file2;

            pets.id_gato = Request["gato"];
            if (pets.id_gato == "")
            {
                pets.id_cachorro = Request["cachorro"];
                pets.id_raca = int.Parse(pets.id_cachorro);
            }
            else
            {
                pets.id_raca = int.Parse(pets.id_gato);
            }
                ModelCliente novopet = new ModelCliente()
                {
                    nome_pet = pets.nome_pet,
                    nasc_pet = pets.nasc_pet,
                    RGA = pets.RGA,
                    id_cli = pets.id_cli,
                    ft_pet = pets.ft_pet,
                    id_raca = pets.id_raca,
                };
                novapetsDAO.InsertPet(novopet);
            

          

            ViewBag.msg = "Cadastro realizado";

            return RedirectToAction("DetalhesCliPet", "Cliente");
        }



        public ActionResult Atualizar()
        {
            string Login = Session["ClienteLogado"] as string;

            CarregaRacaGato();
            CarregaRacaCachorro();
            var metodopet = new PetDAO();
            var pet = metodopet.ListarPetCli(Login);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);

        }
        [HttpPost]
        public ActionResult Atualizar(ModelCliente pet)
        {
            if (ModelState.IsValid)
            {
                var metodopet = new PetDAO();
                pet.id_raca = int.Parse(Request["raca"]);
                metodopet.UpdatePet(pet);
                return RedirectToAction("DetalhesCliPet", "Cliente");
            }
            return View(pet);
        }


        public ActionResult Detalhes()
        {
            string Login = Session["ClienteLogado"] as string;

            var metodopet = new PetDAO();
            var pet = metodopet.ListarPetCli(Login);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }
    }
}