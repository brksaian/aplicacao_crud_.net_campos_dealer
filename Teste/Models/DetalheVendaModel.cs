using System.ComponentModel.DataAnnotations;

namespace Teste.Models
{
    public class DetalheVendaModel
    {
        [Key] 
        public int idDetalheVenda { get; set; }
        public int idVenda { get; set; }
        public int idProduto { get; set; }
        public float vlrUnitarioVenda { get; set; }
        public VendaModel Venda { get; set; }
        public ProdutoModel Produto { get; set; }
    }
}
