using System.ComponentModel.DataAnnotations;

namespace Teste.Models
{
    public class ClienteModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo de nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo da cidade é obrigatório")]
        public string Cidade { get; set; }
    }
}
