using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using Teste.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Teste.Repositorio.Cliente;
using Teste.Repositorio.Produto;
using Teste.Repositorio.Vendas;

namespace Teste.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IVendasRepositorio _vendasRepositorio;

        public HomeController(IHttpClientFactory httpClientFactory, IClienteRepositorio clienteRepositorio, IProdutoRepositorio produtoRepositorio, IVendasRepositorio vendasRepositorio)
        {
            _httpClientFactory = httpClientFactory;
            _clienteRepositorio = clienteRepositorio;
            _produtoRepositorio = produtoRepositorio;
            _vendasRepositorio = vendasRepositorio;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public class ClienteInput
        {
            public int idCliente { get; set; }
            public string nmCliente { get; set; }
            public string Cidade { get; set; }
        }

        public class ProdutoInput
        {
            public int idProduto { get; set; }
            public string dscProduto { get; set; }
            public int vlrUnitario { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> LoadData()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                var responseProdutos = await client.GetAsync("https://camposdealer.dev/Sites/TesteAPI/produto");
                responseProdutos.EnsureSuccessStatusCode();
                var produtosJson = await responseProdutos.Content.ReadAsStringAsync();
                string jsonWithoutEscape = produtosJson.Replace("\\", "");
                string jsonP = jsonWithoutEscape.Substring(1, jsonWithoutEscape.Length - 2); ;
                List<ProdutoModel> produtos = JsonConvert.DeserializeObject<List<ProdutoModel>>(jsonP);

                foreach(var produto in produtos)
                {
                    produto.idProduto = 0;
                    _produtoRepositorio.Adicionar(produto);
                }

                var responseClientes = await client.GetAsync("https://camposdealer.dev/Sites/TesteAPI/cliente");
                responseClientes.EnsureSuccessStatusCode();
                var clientesJson = await responseClientes.Content.ReadAsStringAsync();
                string jsonC1 = clientesJson.Replace("\\", "");
                string jsonC2 = jsonC1.Substring(1, jsonC1.Length - 2); ;
                List<ClienteInput> clienteInputs = JsonConvert.DeserializeObject<List<ClienteInput>>(jsonC2);

                foreach (var cliente in clienteInputs)
                {
                    ClienteModel clienteModel = new ClienteModel
                    {
                        Nome = cliente.nmCliente,
                        Cidade = cliente.Cidade
                    };
                    _clienteRepositorio.Adicionar(clienteModel);
                }

                var responseVendas = await client.GetAsync("https://camposdealer.dev/Sites/TesteAPI/venda");
                responseVendas.EnsureSuccessStatusCode();
                var vendasJson = await responseVendas.Content.ReadAsStringAsync();
                string jsonV1 = vendasJson.Replace("\\", "");
                string jsonV2 = jsonV1.Substring(1, jsonV1.Length - 2); ;
                List<VendaModel> vendas = JsonConvert.DeserializeObject<List<VendaModel>>(jsonV2);

                foreach (var venda in vendas)
                {
                    venda.idVenda = 0;
                    venda.Cliente = _clienteRepositorio.BuscarId(venda.idCliente);
                    venda.DetalhesVenda = new List<DetalheVendaModel>();
                    _vendasRepositorio.Adicionar(venda);
                }

                TempData["Message"] = "Dados carregados com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao carregar os dados da API: " + ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}