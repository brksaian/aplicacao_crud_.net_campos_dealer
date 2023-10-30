using Teste.Data;
using Teste.Models;

namespace Teste.Repositorio.Vendas
{
    public class VendasRepositorio : IVendasRepositorio
    {
        private readonly BancoContext _bancoContext;
        public VendasRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public VendaModel Adicionar(VendaModel venda)
        {
            // Grava o venda no banco de dados
            _bancoContext.Vendas.Add(venda);
            _bancoContext.SaveChanges();
            return venda;

        }

        public VendaModel BuscaridVenda(int idVenda)
        {
            // Retorna o venda do banco de dados pelo idVenda
            return _bancoContext.Vendas.FirstOrDefault(c => c.idVenda == idVenda);
        }

        public VendaModel Editar(VendaModel venda)
        {
            try
            {
                VendaModel vendaAntigo = this.BuscaridVenda(venda.idVenda);

                if (vendaAntigo == null) throw new Exception("Houve um erro na atualização do venda \n Venda não encontrado");
                // Edita o venda no banco de dados

                vendaAntigo.dthVenda = venda.dthVenda;
                vendaAntigo.qtdVenda = venda.qtdVenda;
                vendaAntigo.vlrTotalVenda = venda.vlrTotalVenda;
                vendaAntigo.DetalhesVenda = venda.DetalhesVenda;
                vendaAntigo.Cliente = venda.Cliente;
                vendaAntigo.dthVenda = venda.dthVenda;
                vendaAntigo.DetalhesVenda = venda.DetalhesVenda;

                _bancoContext.Vendas.Update(vendaAntigo);
                _bancoContext.SaveChanges();
                return vendaAntigo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public List<VendaModel> ListarTodos()
        {
            // Retorna todos os vendas do banco de dados
            return _bancoContext.Vendas.ToList<VendaModel>().OrderBy(c => c.dthVenda).ToList();
        }

        public bool Remover(int idVenda)
        {
            try
            {
                VendaModel vendaAntigo = this.BuscaridVenda(idVenda);

                if (vendaAntigo == null) throw new Exception("Houve um erro na deleção do venda. \nVenda não encontrado");
                // Edita o venda no banco de dados
                _bancoContext.Vendas.Remove(vendaAntigo);
                _bancoContext.DetalheVendas.RemoveRange(vendaAntigo.DetalhesVenda);
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
