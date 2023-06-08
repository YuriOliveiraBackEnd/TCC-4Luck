using bibliotecaDAO;
using bibliotecaModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlanosPets.Controllers
{
    public class RacaController : Controller
    {
        // GET: Raca
        
        public ActionResult Index()
        {
            if (Session["FuncLogado"] == null)
            {
                return RedirectToAction("SemAcesso", "Login");
            }
            else
            {
                var metodoRaca = new RacaDAO();
                var listaRaca = metodoRaca.Listar();
                return View(listaRaca);
            }
        }

        public ActionResult Cadastrar()
        {
            if (Session["FuncLogado"] == null)
            {
                return RedirectToAction("SemAcesso", "Login");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Cadastrar(ModelRacas raca, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
                return View(raca);
            RacaDAO novaRacaDAO = new RacaDAO();
            string nome = new RacaDAO().SelectNomeRaca(raca.nome_raca);
            if (nome == raca.nome_raca)
            {
                ViewBag.Raca = "raça já cadastrada";
                return View(raca);
            }
            string Email = Session["FuncLogado"] as string;
            string id = new ProdutoDAO().SelectIdDofunc(Email);
            raca.id_func = id;

            string arquivo = Path.GetFileName(file.FileName);

            string file2 = "/images/" + Path.GetFileName(file.FileName);

            string _path = Path.Combine(Server.MapPath("~/images"), arquivo);

            file.SaveAs(_path);

            raca.ft_raca = file2;

            ModelRacas novaraca = new ModelRacas()
            {
                nome_raca= raca.nome_raca,
                ft_raca = raca.ft_raca,
                tipo_animal = raca.tipo_animal,
                id_func = raca.id_func,
            };
            novaRacaDAO.InsertRaca(novaraca);

            return RedirectToAction("Index");
        }


        public ActionResult ExcluirRaca(int id)
        {
            var novaRacaDAO = new RacaDAO();
            novaRacaDAO.DeleteRaca(id);
            return RedirectToAction("Index");
        }


    }
}