using System.ComponentModel.DataAnnotations;

namespace webApiVS.Models
{
    public class ContatoModel
    {
        [Key]
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public DateTime Data { get; set; } = DateTime.Now.ToLocalTime();
    }
}

