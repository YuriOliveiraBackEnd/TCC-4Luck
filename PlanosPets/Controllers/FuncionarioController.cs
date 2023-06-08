using bibliotecaDAO;
using bibliotecaModel;
using PlanosPets.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;



namespace PlanosPets.Controllers
{
    public class FuncionarioController : Controller
    {
        // GET: Funcionario
        public ActionResult Index()
        {
            if (Session["FuncLogado"] == null)
            {
                return RedirectToAction("SemAcesso", "Login");
            }
            else
            {
                var metodoFuncionario = new FuncionarioDAO();
                var listaFuncionario = metodoFuncionario.Listar();
                return View(listaFuncionario);
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
                return View();
            }
        }



        [HttpPost]
        public ActionResult Cadastrar(ModelFuncionario funcionario)
        {
            if (!ModelState.IsValid)
            {
                FuncionarioDAO novoFuncionarioDAO = new FuncionarioDAO();
                string cpf = new FuncionarioDAO().SelectCPFFunc(funcionario.CPF_func);
                string email = new FuncionarioDAO().SelectEmailFunc(funcionario.email_func);
                if (cpf == funcionario.CPF_func && email == funcionario.email_func)
                {
                    ViewBag.Email = "Email já existente";
                    ViewBag.CPF = "CPF já existente";
                    return View(funcionario);
                }


                else if (cpf == funcionario.CPF_func)
                {
                    ViewBag.CPF = "CPF já existente";
                    return View(funcionario);
                }



                else if (email == funcionario.email_func)
                {
                    ViewBag.Email = "Email já existente";
                    return View(funcionario);
                };





                ModelFuncionario novoFuncionario = new ModelFuncionario()
                {
                    nome_func = funcionario.nome_func,
                    email_func = funcionario.email_func,
                    CPF_func = funcionario.CPF_func,
                    cep_func = funcionario.cep_func,
                    num_func = funcionario.num_func,
                    logradouro_func = funcionario.logradouro_func,
                    nasc_func = funcionario.nasc_func,
                    tel_func = funcionario.tel_func,
                    senha_func = funcionario.senha_func
                };
                novoFuncionarioDAO.InsertFuncionario(novoFuncionario);

                return RedirectToAction("Index");
            }
            return View(funcionario);
        }
       



      
           
        public ActionResult Atualizar(int id)
        {
            if (Session["FuncLogado"] == null)
            {
                return RedirectToAction("SemAcesso", "Login");
            }
            else
            {
                var metodoFuncionario = new FuncionarioDAO();
                var funcionario = metodoFuncionario.ListarId(id);
                if (funcionario == null)
                {
                    return HttpNotFound();
                }
                return View(funcionario);
            }
        }



        [HttpPost]
        public ActionResult AtualizarPerfil(ModelFuncionario funcionario)
        {
            if (!ModelState.IsValid)
            {
                var metodoFuncionario = new FuncionarioDAO();
                metodoFuncionario.UpdateFuncionario(funcionario);
                return RedirectToAction("Index");
            }
            return View(funcionario);
        }
        public ActionResult AtualizarPerfil(int id)
        {
            if (Session["FuncLogado"] == null)
            {
                return RedirectToAction("SemAcesso", "Login");
            }
            else
            {
                var metodoFuncionario = new FuncionarioDAO();
                var funcionario = metodoFuncionario.ListarId(id);
                if (funcionario == null)
                {
                    return HttpNotFound();
                }
                return View(funcionario);
            }
        }



        [HttpPost]
        public ActionResult Atualizar(ModelFuncionario funcionario)
        {
            if (!ModelState.IsValid)
            {
                var metodoFuncionario = new FuncionarioDAO();
                metodoFuncionario.UpdateFuncionario(funcionario);
                return RedirectToAction("Index");
            }
            return View(funcionario);
        }


        public ActionResult ExcluirFunc(int id)
        {
            var metodoFunc = new FuncionarioDAO();
            metodoFunc.DeleteFunc(id);
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
                var metodoFuncionario = new FuncionarioDAO();
                var funcionario = metodoFuncionario.ListarId(id);
                if (funcionario == null)
                {
                    return HttpNotFound();
                }
                return View(funcionario);
            }
        }

        public ActionResult DetalhesEmail()
        {
            string Login = Session["FuncLogado"] as string;

            var metodoFuncionario = new FuncionarioDAO();
            var funcionario = metodoFuncionario.ListarEmail(Login);
            if (funcionario == null)
            {
                return HttpNotFound();
            }
            return View(funcionario);
        }
    }
}


