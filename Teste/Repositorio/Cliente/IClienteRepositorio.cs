using Teste.Models;

namespace Teste.Repositorio.Cliente
{
    public interface IClienteRepositorio
    {
        ClienteModel Adicionar(ClienteModel cliente);
        List<ClienteModel> ListarTodos();
        ClienteModel BuscarId(int Id);
        ClienteModel Editar(ClienteModel cliente);
        bool Remover(int Id);
    }
}
