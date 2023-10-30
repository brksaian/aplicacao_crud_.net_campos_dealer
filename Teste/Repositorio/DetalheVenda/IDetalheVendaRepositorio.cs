using Teste.Models;

namespace Teste.Repositorio.DetalheVenda
{
    public interface IDetalheVendaRepositorio
    {
        DetalheVendaModel Adicionar(DetalheVendaModel cliente);
        List<DetalheVendaModel> ListarTodos();
        DetalheVendaModel BuscaridDetalheVenda(int Id);
        List<DetalheVendaModel> BuscaridVenda(int Id);
        DetalheVendaModel Editar(DetalheVendaModel cliente);
        bool Remover(int Id);
    }
}
