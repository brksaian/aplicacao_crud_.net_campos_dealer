using Teste.Data;
using Teste.Models;

namespace Teste.Repositorio.Cliente
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly BancoContext _bancoContext;
        public ClienteRepositorio(BancoContext bancoContext) {
            _bancoContext = bancoContext;
        }
        public ClienteModel Adicionar(ClienteModel cliente)
        {
            // Grava o cliente no banco de dados
            _bancoContext.Clientes.Add(cliente);
            _bancoContext.SaveChanges();
            return cliente;

        }
    }
}
