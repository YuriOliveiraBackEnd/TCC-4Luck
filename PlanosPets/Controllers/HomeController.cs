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
    public class HomeController : Controller
    {

        //public ActionResult ListaRaca()
        //{ 
        //        var metodoRaca = new RacaDAO();
        //        var listaRaca = metodoRaca.Listar();
        //        return View(listaRaca);    
        //}
        public ActionResult Index()
        {
            var metodoProduto = new ProdutoDAO();
            var produto = metodoProduto.Listar();
            return View(produto);
        }
        public ActionResult ListarPlanos()
        {
            var metodoProduto = new ProdutoDAO();
            var produto = metodoProduto.ListarPlanos();
            return View(produto);
        }
        public ActionResult IndexFunc()
        {
            if (Session["FuncLogado"] == null)
            {
                return RedirectToAction("SemAcesso", "Login");
            }
            else
            {
                var metodoProduto = new ProdutoDAO();
                var listaProduto = metodoProduto.Listar();
                return View(listaProduto);
            }
        }

        public ActionResult Buscar(string pesquisar)
        {
            if (pesquisar == "Planos")
                return RedirectToAction("Planos");

            else
            {
                var metodoProduto = new ProdutoDAO();
                var produto = metodoProduto.Pesquisa(pesquisar);
                if (produto == null)
                {
                    return HttpNotFound();
                }
                else
                    return View(produto);
            }

        }
        public ActionResult Detalhes(int id)
        {

            var metodoProduto = new ProdutoDAO();
            var produto = metodoProduto.ListarId(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);

        }
    }
}