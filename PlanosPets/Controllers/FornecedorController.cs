using bibliotecaDAO;
using bibliotecaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlanosPets.Controllers
{
    public class FornecedorController : Controller
    {
        // GET: Fornecedor
        public ActionResult Index()
        {
            if (Session["FuncLogado"] == null)
            {
                return RedirectToAction("SemAcesso", "Login");
            }
            else
            {
                var metodoFornecedor = new FornecedorDAO();
                var listaFornecedor = metodoFornecedor.Listar();
                return View(listaFornecedor);
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
        public ActionResult Cadastrar(ModelFornecedor fornecedor)
        {
            if (!ModelState.IsValid)
            {
                return View(fornecedor);
            }
            FornecedorDAO novoFornecedorDAO = new FornecedorDAO();
                string cnpj = new FornecedorDAO().SelectCNPJFonc(fornecedor.CNPJ_forn);
                string email = new FornecedorDAO().SelectEmailForn(fornecedor.email_forn);
                if (cnpj == fornecedor.CNPJ_forn && email == fornecedor.email_forn)
                {
                    ViewBag.Email = "Email já cadastrado";
                    ViewBag.CNPJ = "CNPJ já cadastrado";
                    return View(fornecedor);
                }


                else if (cnpj == fornecedor.CNPJ_forn)
                {
                    ViewBag.CPF = "CNPJ já cadastrado";
                    return View(fornecedor);
                }



                else if (email == fornecedor.email_forn)
                {
                    ViewBag.Email = "Email já cadastrado";
                    return View(fornecedor);
                };


                ModelFornecedor novofornecedor = new ModelFornecedor()
                {
                    nome_forn = fornecedor.nome_forn,
                    email_forn = fornecedor.email_forn,
                    CNPJ_forn = fornecedor.CNPJ_forn,
                    cep_forn = fornecedor.cep_forn,
                    num_forn = fornecedor.num_forn,
                    logradouro_forn = fornecedor.logradouro_forn,
                    id_prod = fornecedor.id_prod,
                    tel_forn = fornecedor.tel_forn
                    
                };
                novoFornecedorDAO.InsertFornecedor(novofornecedor);

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
                var metodoFornecedor = new FornecedorDAO();
                var fornecedor = metodoFornecedor.ListarId(id);
                if (fornecedor == null)
                {
                    return HttpNotFound();
                }
                return View(fornecedor);
            }
        }

        [HttpPost]
        public ActionResult Atualizar(ModelFornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                var metodoFornecedor = new FornecedorDAO();
                metodoFornecedor.Updatefornecedor(fornecedor);
                return RedirectToAction("Index");
            }
            return View(fornecedor);
        }

        public ActionResult ExcluirForn(int id)
        {
            var metodoFornecedor = new FornecedorDAO();
            metodoFornecedor.DeleteForn(id);
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
                var metodoFornecedor = new FornecedorDAO();
                var fornecedor = metodoFornecedor.ListarId(id);
                if (fornecedor == null)
                {
                    return HttpNotFound();
                }
                return View(fornecedor);
            }
        }
    }
}