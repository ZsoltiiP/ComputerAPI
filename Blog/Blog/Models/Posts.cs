using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogApi.Models
{
    public class Posts
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(20)")]
        [Required]
        public string Category { get; set; }

        [Column(TypeName = "text")]
        public string Post { get; set; }
        public DateTime RegTime { get; set; } = DateTime.Now;
        public DateTime ModTime { get; set; } = DateTime.Now;
        public int BloggerId { get; set; }
        //Kapcsolatok miatt
        [JsonIgnore]
        public virtual Blogger Blogger { get; set; }
    }
}
