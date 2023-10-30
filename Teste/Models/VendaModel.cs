using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Teste.Models
{
    public class VendaModel
    {
        [Key]
        public int idVenda { get; set; }
        public int idCliente { get; set; }
        public int qtdVenda { get; set; }
        public DateTime dthVenda { get; set; }
        public float vlrTotalVenda { get; set; }
        public List<DetalheVendaModel> DetalhesVenda { get; set; } = new List<DetalheVendaModel>();
        public ClienteModel Cliente { get; set; }
    }
}
