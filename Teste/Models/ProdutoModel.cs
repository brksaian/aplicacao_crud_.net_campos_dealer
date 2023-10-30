using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace Teste.Models
{
    public class ProdutoModel
    {
        [Key]
        [JsonProperty("idProduto")]
        public int idProduto { get; set; }

        [Required(ErrorMessage = "O campo de descrição do produto é obrigatório")]
        [JsonProperty("dscProduto")]
        public string dscProduto { get; set; }

        [Required(ErrorMessage = "O campo do valor do produto é obrigatório")]
        [Range(0.00, float.MaxValue, ErrorMessage = "O valor deve ser maior ou igual a R$0.00")]
        [JsonProperty("vlrUnitario")]
        public float vlrUnitario { get; set; }
    }


}
