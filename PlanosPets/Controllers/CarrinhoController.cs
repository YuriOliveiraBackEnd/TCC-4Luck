using bibliotecaDAO;
using bibliotecaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlanosPets.Controllers
{
    public class CarrinhoController : Controller
    {
        ProdutoDAO ac = new ProdutoDAO();
        VendaDAO acV = new VendaDAO();
        ItemCarrinhoDAO acI = new ItemCarrinhoDAO();
        // GET: Carrinho
        public ActionResult Index()
        {
            return View();
        }

        //private Context db = new Context();

        public static string codigo;

        public ActionResult AdicionarCarrinho(int id, double pre)
        {
            ModelVenda carrinho = Session["Carrinho"] != null ? (ModelVenda)Session["Carrinho"] : new ModelVenda();
            var produto = ac.GetConsProd(id);
            codigo = id.ToString();

            ModelProduto prod = new ModelProduto();

            if (produto != null)
            {
                var itemPedido = new ModelItemCarrinho();
                itemPedido.ItemPedidoID = Guid.NewGuid();
                itemPedido.ProdutoID = id.ToString();
                itemPedido.Produto = produto[0].nome_prod;
                itemPedido.Qtd = 1;
                itemPedido.valorUnit = pre;
                itemPedido.Foto_produto = produto[0].ft_prod;
                

                List<ModelItemCarrinho> x = carrinho.ItensPedido.FindAll(l => l.Produto == itemPedido.Produto);

                if (x.Count != 0)
                {
                    carrinho.ItensPedido.FirstOrDefault(p => p.Produto == produto[0].nome_prod).Qtd += 1;
                    itemPedido.valorParcial = itemPedido.Qtd * itemPedido.valorUnit;
                    carrinho.ValorTotal += itemPedido.valorParcial;
                    carrinho.ItensPedido.FirstOrDefault(p => p.Produto == produto[0].nome_prod).valorParcial = carrinho.ItensPedido.FirstOrDefault(p => p.Produto == produto[0].nome_prod).Qtd * itemPedido.valorUnit;

                }

                else
                {
                    itemPedido.valorParcial = itemPedido.Qtd * itemPedido.valorUnit;
                    carrinho.ValorTotal += itemPedido.valorParcial;
                    carrinho.ItensPedido.Add(itemPedido);
                }

                /*carrinho.ValorTotal = carrinho.ItensPedido.Select(i => i.Produto).Sum(d => d.Valor);*/

                Session["Carrinho"] = carrinho;
            }

            return RedirectToAction("Carrinho");
        }

        public ActionResult Carrinho()
        {
            ModelVenda carrinho = Session["Carrinho"] != null ? (ModelVenda)Session["Carrinho"] : new ModelVenda();

            return View(carrinho);
        }

        public ActionResult ExcluirItem(Guid id)
        {
            var carrinho = Session["Carrinho"] != null ? (ModelVenda)Session["Carrinho"] : new ModelVenda();
            var itemExclusao = carrinho.ItensPedido.FirstOrDefault(i => i.ItemPedidoID == id);

            carrinho.ValorTotal -= itemExclusao.valorParcial;

            carrinho.ItensPedido.Remove(itemExclusao);

            Session["Carrinho"] = carrinho;
            return RedirectToAction("Carrinho");
        }

        public ActionResult SalvarCarrinho(ModelVenda x)
        {

            if ((Session["ClienteLogado"] == null) || (Session["senhaLogado"] == null))

            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return RedirectToAction("confVenda");
            }
        }
        public ActionResult confVenda()
        {
            return View();
        }
        [HttpPost]
        public ActionResult confVenda(ModelVenda x)
        {
            string Email = Session["ClienteLogado"] as string;
            string id = new PetDAO().SelectIdDoCli(Email);
            x.id_cli = id;
            var carrinho = Session["Carrinho"] != null ? (ModelVenda)Session["Carrinho"] : new ModelVenda();
            x.pagamento = x.pagamento;
            ModelVenda venda = new ModelVenda();
            
            venda.data_venda = DateTime.Now.ToLocalTime().ToString("dd/MM/yyyy");
            venda.horaVenda = DateTime.Now.ToLocalTime().ToString("HH:mm");
            venda.id_cli = id;
            venda.ValorTotal = carrinho.ValorTotal;
            venda.pagamento = "pix";
            acV.InsertVenda(venda);

            ModelItemCarrinho mdV = new ModelItemCarrinho();
            acV.buscaIdVenda(x);

            for (int i = 0; i < carrinho.ItensPedido.Count; i++)
            {

                mdV.PedidoID = x.id_venda;
                mdV.ProdutoID = carrinho.ItensPedido[i].ProdutoID;
                mdV.Qtd = carrinho.ItensPedido[i].Qtd;
                mdV.valorParcial = carrinho.ItensPedido[i].valorParcial;
                acI.inserirItem(mdV);
                //acI.BaixaEstoque();
            }

            carrinho.ValorTotal = 0;
            carrinho.ItensPedido.Clear();
            return RedirectToAction("vendaRealizada");
        }
        public ActionResult vendaRealizada()
        {
            return View();
        }
        public ActionResult VendaHistorico()
        {
            if (Session["FuncLogado"] == null)
            {
                return RedirectToAction("SemAcesso", "Login");
            }
            else
            {
                var metodovenda = new VendaDAO();
                var listavenda= metodovenda.Listar();
                return View(listavenda);
            }
        }
    }
}