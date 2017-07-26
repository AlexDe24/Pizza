using System.ComponentModel.DataAnnotations;

namespace Pizza.Logic
{
    public class Status
    {
        public int id { get; set; }

        [Required]
        public string name { get; set; }
    }
}
