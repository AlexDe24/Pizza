using System.ComponentModel.DataAnnotations;

namespace Pizza.Logic.DTO
{
    public class Status
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
