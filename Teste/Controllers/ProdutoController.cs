using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Teste.Models;
using Teste.Repositorio.Produto;

namespace Teste.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        public ProdutoController(IProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
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
            List<ProdutoModel> produtos = _produtoRepositorio.ListarTodos();
            return View(produtos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(ProdutoModel produto)
        {
            // Chama o repositório para gravar o produto no banco de dados
            try
            {
                if (ModelState.IsValid)
                {
                    if (ModelState.IsValid)
                    {
                        if (Decimal.TryParse(produto.vlrUnitario.ToString(), NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal valorDecimal))
                        {
                            produto.vlrUnitario = (float)valorDecimal; // Convertendo de decimal para float

                            _produtoRepositorio.Adicionar(produto);
                            TempData["SuccessMessage"] = "Produto adicionado com sucesso!";
                        }
                        else
                        {
                            ModelState.AddModelError("vlrUnitario", "Valor inválido");
                            return View(produto);
                        }
                    }
                    else
                    {
                        return View(produto);
                    }
                }
                else
                {
                    return View(produto);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int Id)
        {
            try
            {
                ProdutoModel produto = _produtoRepositorio.BuscarId(Id);

                if (produto == null)
                {
                    TempData["ErrorMessage"] = "Produto não encontrado";
                    return RedirectToAction("Index");
                }

                return View(produto);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Editar(ProdutoModel produto)
        {
            // Chama o repositório para gravar o produto no banco de dados
            try
            {
                if (ModelState.IsValid)
                {
                    _produtoRepositorio.Editar(produto);
                    TempData["SuccessMessage"] = "Produto atualizado com sucesso!";
                }
                else
                {
                    return View(produto);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        public IActionResult ApagarConfirmacao(int Id)
        {
            try
            {
                ProdutoModel produto = _produtoRepositorio.BuscarId(Id);

                if (produto == null)
                {
                    TempData["ErrorMessage"] = "Produto não encontrado";
                    return RedirectToAction("Index");
                }

                return View(produto);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult Apagar(int Id)
        {
            // Chama o repositório para apagar o produto no banco de dados
            try
            {
                _produtoRepositorio.Remover(Id);
                TempData["SuccessMessage"] = "Produto removido com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
