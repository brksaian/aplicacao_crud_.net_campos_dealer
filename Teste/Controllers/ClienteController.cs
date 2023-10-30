using Microsoft.AspNetCore.Mvc;
using Teste.Models;
using Teste.Repositorio.Cliente;

namespace Teste.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        public ClienteController(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
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
            List<ClienteModel> clientes = _clienteRepositorio.ListarTodos();
            return View(clientes);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(ClienteModel cliente)
        {
            // Chama o repositório para gravar o cliente no banco de dados
            try
            {
                if (ModelState.IsValid)
                {
                    _clienteRepositorio.Adicionar(cliente);
                    TempData["SuccessMessage"] = "Cliente adicionado com sucesso!";
                }
                else
                {
                    return View(cliente);
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
                ClienteModel cliente = _clienteRepositorio.BuscarId(Id);

                if (cliente == null)
                {
                    TempData["ErrorMessage"] = "Cliente não encontrado";
                    return RedirectToAction("Index");
                }

                return View(cliente);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Editar(ClienteModel cliente)
        {
            // Chama o repositório para gravar o cliente no banco de dados
            try
            {
                if (ModelState.IsValid)
                {
                    _clienteRepositorio.Editar(cliente);
                    TempData["SuccessMessage"] = "Cliente atualizado com sucesso!";
                }
                else
                {
                    return View(cliente);
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
                ClienteModel cliente = _clienteRepositorio.BuscarId(Id);

                if (cliente == null)
                {
                    TempData["ErrorMessage"] = "Cliente não encontrado";
                    return RedirectToAction("Index");
                }

                return View(cliente);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult Apagar(int Id)
        {
            // Chama o repositório para apagar o cliente no banco de dados
            try
            {
                _clienteRepositorio.Remover(Id);
                TempData["SuccessMessage"] = "Cliente removido com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
