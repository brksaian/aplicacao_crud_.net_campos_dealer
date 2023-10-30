using Microsoft.AspNetCore.Mvc;
using Teste.Models;
using Teste.Repositorio.Cliente;
using Teste.Repositorio.DetalheVenda;
using Teste.Repositorio.Produto;
using Teste.Repositorio.Vendas;

namespace Teste.Controllers
{
    public class VendaController : Controller
    {
        private readonly IVendasRepositorio _vendaRepositorio;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IDetalheVendaRepositorio _detalheVendaRepositorio;
        public VendaController(IVendasRepositorio vendaRepositorio, IClienteRepositorio clienteRepositorio, IProdutoRepositorio produtoRepositorio, IDetalheVendaRepositorio detalheVendaRepositorio)
        {
            _vendaRepositorio = vendaRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _produtoRepositorio = produtoRepositorio;
            _detalheVendaRepositorio = detalheVendaRepositorio;
        }
        public IActionResult Index()
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }
            if (TempData["SuccessMessage"] != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
            }
            List<VendaModel> vendas = _vendaRepositorio.ListarTodos();
            foreach (var item in vendas)
            {
                item.Cliente = _clienteRepositorio.BuscarId(item.idCliente);
                item.DetalhesVenda = _detalheVendaRepositorio.BuscaridVenda(item.idVenda);
                if(item.DetalhesVenda != null && item.DetalhesVenda.Any())
                {
                    foreach (var item2 in item.DetalhesVenda)
                    {
                        item2.Produto = _produtoRepositorio.BuscarId(item2.idProduto);
                    }
                }
            }

            return View(vendas);
        }

        public IActionResult Criar()
        {
            List<ClienteModel> clientes = _clienteRepositorio.ListarTodos();
            List<ProdutoModel> produtos = _produtoRepositorio.ListarTodos();
            ViewData["Clientes"] = clientes;
            ViewData["Produtos"] = produtos;

            return View();
        }

        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Criar([FromBody] VendaModel venda)
        {
            // Chama o repositório para gravar o venda no banco de dados
            try
            {
                List<DetalheVendaModel> detalhesVenda = new List<DetalheVendaModel>();

                float vlrTotalVenda = 0;
                int qtdVenda = 0;

                foreach (DetalheVendaModel item in venda.DetalhesVenda)
                {
                    ProdutoModel produto = _produtoRepositorio.BuscarId(item.idProduto);
                    item.Produto = produto;
                    vlrTotalVenda += item.qtdVenda * item.vlrUnitarioVenda;
                    qtdVenda += item.qtdVenda;
                }

                venda.Cliente = _clienteRepositorio.BuscarId(venda.idCliente);

                venda.dthVenda = DateTime.Now;

                venda.vlrTotalVenda = vlrTotalVenda;

                venda.qtdVenda = qtdVenda;

                _vendaRepositorio.Adicionar(venda);

                foreach (DetalheVendaModel item in venda.DetalhesVenda)
                {
                    item.idVenda = venda.idVenda;
                    _detalheVendaRepositorio.Editar(item);
                }

                TempData["SuccessMessage"] = "Venda adicionado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return Json(new { success = false, errorMessage = ex.Message });
            }
        }

        public IActionResult Editar(int id)
        {
            try
            {
                VendaModel venda = _vendaRepositorio.BuscaridVenda(id);
                venda.Cliente = _clienteRepositorio.BuscarId(venda.idCliente);
                venda.DetalhesVenda = _detalheVendaRepositorio.BuscaridVenda(venda.idVenda);
                if (venda.DetalhesVenda != null && venda.DetalhesVenda.Any())
                {
                    foreach (var item2 in venda.DetalhesVenda)
                    {
                        item2.Produto = _produtoRepositorio.BuscarId(item2.idProduto);
                    }
                }

                    List<ClienteModel> clientes = _clienteRepositorio.ListarTodos();
                    List<ProdutoModel> produtos = _produtoRepositorio.ListarTodos();
                    ViewData["Clientes"] = clientes;
                    ViewData["Produtos"] = produtos;

                if (venda == null)
                {
                    TempData["ErrorMessage"] = "Venda não encontrado";
                    return RedirectToAction("Index");
                }

                return View(venda);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Editar(VendaModel venda)
        {
            // Chama o repositório para gravar o venda no banco de dados
            try
            {
                if (ModelState.IsValid)
                {
                    _vendaRepositorio.Editar(venda);
                    TempData["SuccessMessage"] = "Venda atualizado com sucesso!";
                }
                else
                {
                    return View(venda);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            try
            {
                VendaModel venda = _vendaRepositorio.BuscaridVenda(id);

                if (venda == null)
                {
                    TempData["ErrorMessage"] = "Venda não encontrado";
                    return RedirectToAction("Index");
                }

                return View(venda);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult Apagar(int id)
        {
            // Chama o repositório para apagar o venda no banco de dados
            try
            {
                _vendaRepositorio.Remover(id);
                TempData["SuccessMessage"] = "Venda removido com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
