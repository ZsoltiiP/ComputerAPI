using System.ComponentModel.DataAnnotations;

namespace Computer.Models
{
    public class Computer
    {
        [Key]
        public int CompId { get; set; }
        public string? Brand { get; set; }
        public string? Type { get; set; }
        public double Display { get; set; }
        public int Memory { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public int OsId {  get; set; } 
    }
}
