using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;

namespace BlogApi.Models
{
    public class Blogger
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Password { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public DateTime RegTime { get; set; } = DateTime.Now;
        public DateTime ModDate { get; set; } = DateTime.Now;
        //Kapcsolatok miatt
        public virtual ICollection<Posts> Posts { get; set; } = new List<Posts>();

    }
}
