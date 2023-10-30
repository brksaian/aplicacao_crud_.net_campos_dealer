using Teste.Models;

namespace Teste.Repositorio.Produto
{
    public interface IProdutoRepositorio
    {
        ProdutoModel Adicionar(ProdutoModel produto);
        List<ProdutoModel> ListarTodos();
        ProdutoModel BuscarId(int Id);
        ProdutoModel Editar(ProdutoModel produto);
        bool Remover(int Id);
    }
}
