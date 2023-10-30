using Teste.Models;

namespace Teste.Repositorio.Vendas
{
    public interface IVendasRepositorio
    {
        VendaModel Adicionar(VendaModel venda);
        List<VendaModel> ListarTodos();
        VendaModel BuscaridVenda(int Id);
        VendaModel Editar(VendaModel venda);
        bool Remover(int Id);
    }
}
