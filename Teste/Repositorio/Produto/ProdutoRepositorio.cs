using System.Linq;
using Teste.Data;
using Teste.Models;

namespace Teste.Repositorio.Produto
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly BancoContext _bancoContext;
        public ProdutoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public ProdutoModel Adicionar(ProdutoModel produto)
        {
            // Grava o produto no banco de dados
            _bancoContext.Produtos.Add(produto);
            _bancoContext.SaveChanges();
            return produto;

        }

        public ProdutoModel BuscarId(int Id)
        {
            // Retorna o produto do banco de dados pelo Id
            return _bancoContext.Produtos.FirstOrDefault(c => c.idProduto == Id);
        }

        public ProdutoModel Editar(ProdutoModel produto)
        {
            try
            {
                ProdutoModel produtoAntigo = this.BuscarId(produto.idProduto);

                if (produtoAntigo == null) throw new Exception("Houve um erro na atualização do produto \nProduto não encontrado");
                // Edita o produto no banco de dados

                produtoAntigo.dscProduto = produto.dscProduto;
                produtoAntigo.vlrUnitario = produto.vlrUnitario;

                _bancoContext.Produtos.Update(produtoAntigo);
                _bancoContext.SaveChanges();
                return produtoAntigo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public List<ProdutoModel> ListarTodos()
        {
            // Retorna todos os produtos do banco de dados
            return _bancoContext.Produtos.ToList<ProdutoModel>();
        }

        public bool Remover(int Id)
        {
            try
            {
                ProdutoModel produtoAntigo = this.BuscarId(Id);

                if (produtoAntigo == null) throw new Exception("Houve um erro na deleção do produto. \nProduto não encontrado");
                // Edita o produto no banco de dados
                _bancoContext.Produtos.Remove(produtoAntigo);
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
