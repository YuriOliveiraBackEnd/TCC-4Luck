using bibliotecaDAO;
using bibliotecaModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlanosPets.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto

        public void CarregaCategoria()
        {
            List<SelectListItem> categoria = new List<SelectListItem>();



            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=db4luck;User=root;pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Categorias", con);
                MySqlDataReader rdr = cmd.ExecuteReader();



                while (rdr.Read())
                {
                    categoria.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
            }
            ViewBag.categoria = new SelectList(categoria, "Value", "Text");
        }
        public ActionResult ListaProduto()
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
        [HttpGet]
        public ActionResult Cadastrar()
        {
            if (Session["FuncLogado"] == null)
            {
                return RedirectToAction("SemAcesso", "Login");
            }
            else
            {
                CarregaCategoria();
                return View();
            }
        }
        [HttpPost]
        public ActionResult Cadastrar(ModelProduto produto, HttpPostedFileBase file)
        {
            var metodoProduto = new ProdutoDAO();
            string Email = Session["FuncLogado"] as string;
            string id = new ProdutoDAO().SelectIdDofunc(Email);
            produto.id_func = int.Parse(id);

            string arquivo = Path.GetFileName(file.FileName);

            string file2 = "/images/" + Path.GetFileName(file.FileName);

            string _path = Path.Combine(Server.MapPath("~/images"), arquivo);

            file.SaveAs(_path);

            produto.ft_prod = file2;



            ModelProduto novoproduto = new ModelProduto()
            {
                nome_prod = produto.nome_prod,
                valor_unitario = produto.valor_unitario,
                quant = produto.quant,
                desc_prod = produto.desc_prod,
                ft_prod = produto.ft_prod,
                id_func = produto.id_func,
                id_categoria = int.Parse(Request["categoria"]),

            };

            metodoProduto.InsertProduto(novoproduto);

            ViewBag.msg = "Cadastro realizado";

            return RedirectToAction("ListaProduto");

        }



        public ActionResult Atualizar(int id)
        {
            if (Session["FuncLogado"] == null)
            {
                return RedirectToAction("SemAcesso", "Login");
            }
            else
            {
                CarregaCategoria();
                var metodoproduto = new ProdutoDAO();
                var produto = metodoproduto.ListarId(id);
                if (produto == null)
                {
                    return HttpNotFound();
                }
                return View(produto);
            }
        }



        [HttpPost]
        public ActionResult Atualizar(ModelProduto produto)
        {
            if (ModelState.IsValid)
            {
                var metodoproduto = new ProdutoDAO();
                produto.tipo_cate = Request["categoria"];
                if (produto.tipo_cate != "")
                {
                    produto.id_categoria = int.Parse(produto.tipo_cate);
                }
                produto.id_categoria= produto.id_categoria;
                metodoproduto.UpdateProduto(produto);
                return RedirectToAction("ListaProduto");
            }
            return View(produto);
        }
        public ActionResult ExcluirProd(int id)
        {
            var metodoproduto = new ProdutoDAO();
            metodoproduto.DeleteProd(id);
            return RedirectToAction("ListaProduto");
        }

        public ActionResult Detalhes(int id)
        {
            if (Session["FuncLogado"] == null)
            {
                return RedirectToAction("SemAcesso", "Login");
            }
            else
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
        public ActionResult ListarProdDog()
        {
            var metodoProduto = new ProdutoDAO();
            var produto = metodoProduto.Listar();
            return View(produto);
        }
        public ActionResult ListarProCat()
        {
            var metodoProduto = new ProdutoDAO();
            var produto = metodoProduto.Listar();
            return View(produto);
        }
    }
}