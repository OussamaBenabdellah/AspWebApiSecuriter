using System.ComponentModel.DataAnnotations.Schema;

namespace AspWebApiSecuriter.Data.Models
{
    public class Personne
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } 
        public string Name { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime? Birthday { get; set; }
        public string? Address { get; internal set; }
        public string DisplayId { get; internal set; }
    }
}
