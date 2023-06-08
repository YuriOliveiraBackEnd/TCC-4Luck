using bibliotecaDAO;
using bibliotecaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlanosPets.Controllers
{
    public class CategoriaController : Controller
    {
        public ActionResult Index()
        {
            if (Session["FuncLogado"] == null)
            {
                return RedirectToAction("SemAcesso", "Login");
            }
            else
            {
                var metodoCategoria = new CategoriaDAO();
                var listaCategoria = metodoCategoria.Listar();
                return View(listaCategoria);
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
        public ActionResult Cadastrar(ModelCategorias categorias)
        {
            if (!ModelState.IsValid)
                return View(categorias);
            CategoriaDAO novoCategoriaDAO = new CategoriaDAO();
            string nome = new CategoriaDAO().SelectNomeCategoria(categorias.nome_categoria);
            if (nome == categorias.nome_categoria)
            {
                ViewBag.Categoria = "Categoria já cadastrada";
                return View(categorias);
            }


            ModelCategorias novacategoria = new ModelCategorias()
            {
                nome_categoria = categorias.nome_categoria,
                desc_categoria = categorias.desc_categoria,
            };
            novoCategoriaDAO.InsertCategoria(novacategoria);

            return RedirectToAction("Index");
        }

        public ActionResult Atualizar(int id)
        {
            if (Session["FuncLogado"] == null)
            {
                return RedirectToAction("SemAcesso", "Login");
            }

            else
            {
                var metodoCategoria = new CategoriaDAO();
                var cliente = metodoCategoria.ListarId(id);


                if (cliente == null)
                {
                    return HttpNotFound();
                }
                return View(cliente); 
            }
        }

        [HttpPost]
        public ActionResult Atualizar(ModelCategorias categoria)
        {
            if (ModelState.IsValid)
            {
                var metodoCategoria = new CategoriaDAO();
                metodoCategoria.UpdateCategoria(categoria);
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        public ActionResult ExcluirCat(int id)
        {
            var metodoCategoria = new CategoriaDAO();
            metodoCategoria.DeleteCategoria(id);
            return RedirectToAction("Index");

        }

        public ActionResult Detalhes(int id)
        {
            if (Session["FuncLogado"] == null)
            {
                return RedirectToAction("SemAcesso", "Login");
            }
            else
            {


                var metodoCategoria = new CategoriaDAO();
                var categoria = metodoCategoria.ListarId(id);
                if (categoria == null)
                {
                    return HttpNotFound();
                }
                return View(categoria);
            }
        }

    }
}