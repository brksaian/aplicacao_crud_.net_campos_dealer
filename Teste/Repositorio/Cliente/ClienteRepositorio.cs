using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public ClienteModel BuscarId(int Id)
        {
            // Retorna o cliente do banco de dados pelo Id
            return _bancoContext.Clientes.FirstOrDefault(c => c.Id == Id);
        }

        public ClienteModel Editar(ClienteModel cliente)
        {
            try
            {
                ClienteModel clienteAntigo = this.BuscarId(cliente.Id);

                if (clienteAntigo == null) throw new Exception("Houve um erro na atualização do cliente \n Cliente não encontrado");
                // Edita o cliente no banco de dados

                clienteAntigo.Nome = cliente.Nome;
                clienteAntigo.Cidade = cliente.Cidade;

                _bancoContext.Clientes.Update(clienteAntigo);
                _bancoContext.SaveChanges();
                return clienteAntigo;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public List<ClienteModel> ListarTodos()
        {
            // Retorna todos os clientes do banco de dados
            return _bancoContext.Clientes.ToList<ClienteModel>().OrderBy(c => c.Nome).ToList();
        }

        public bool Remover(int Id)
        {
            try
            {
                ClienteModel clienteAntigo = this.BuscarId(Id);

                if (clienteAntigo == null) throw new Exception("Houve um erro na deleção do cliente. \nCliente não encontrado");
                // Edita o cliente no banco de dados
                _bancoContext.Clientes.Remove(clienteAntigo);
                _bancoContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
