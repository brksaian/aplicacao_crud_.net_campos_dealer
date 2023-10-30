using Teste.Data;
using Teste.Models;

namespace Teste.Repositorio.DetalheVenda
{
    public class DetalheVendaRepositorio : IDetalheVendaRepositorio
    {
        private readonly BancoContext _bancoContext;
        public DetalheVendaRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public DetalheVendaModel Adicionar(DetalheVendaModel detalheVenda)
        {
            // Grava o detalheVenda no banco de dados
            _bancoContext.DetalheVendas.Add(detalheVenda);
            _bancoContext.SaveChanges();
            return detalheVenda;

        }

        public DetalheVendaModel BuscaridDetalheVenda(int idDetalheVenda)
        {
            // Retorna o detalheVenda do banco de dados pelo idDetalheVenda
            return _bancoContext.DetalheVendas.FirstOrDefault(c => c.idDetalheVenda == idDetalheVenda);
        }

        public List<DetalheVendaModel> BuscaridVenda(int idVenda)
        {
            // Retorna o detalheVenda do banco de dados pelo idVenda
            return _bancoContext.DetalheVendas.ToList().Where(c => c.idVenda == idVenda).ToList();
        }

        public DetalheVendaModel Editar(DetalheVendaModel detalheVenda)
        {
            try
            {
                DetalheVendaModel detalheVendaAntigo = this.BuscaridDetalheVenda(detalheVenda.idDetalheVenda);

                if (detalheVendaAntigo == null) throw new Exception("Houve um erro na atualização do detalheVenda \n Cliente não encontrado");
                // Edita o detalheVenda no banco de dados

                detalheVendaAntigo.qtdVenda = detalheVenda.qtdVenda;
                detalheVendaAntigo.vlrUnitarioVenda = detalheVenda.vlrUnitarioVenda;
                detalheVendaAntigo.idProduto = detalheVenda.idProduto;
                detalheVendaAntigo.Produto = detalheVenda.Produto;
                detalheVendaAntigo.Produto = detalheVenda.Produto;

                _bancoContext.DetalheVendas.Update(detalheVendaAntigo);
                _bancoContext.SaveChanges();
                return detalheVendaAntigo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public List<DetalheVendaModel> ListarTodos()
        {
            // Retorna todos os detalheVendas do banco de dados
            return _bancoContext.DetalheVendas.ToList<DetalheVendaModel>();
        }

        public bool Remover(int idDetalheVenda)
        {
            try
            {
                DetalheVendaModel detalheVendaAntigo = this.BuscaridDetalheVenda(idDetalheVenda);

                if (detalheVendaAntigo == null) throw new Exception("Houve um erro na deleção do detalheVenda. \nCliente não encontrado");
                // Edita o detalheVenda no banco de dados
                _bancoContext.DetalheVendas.Remove(detalheVendaAntigo);
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
