using System.ComponentModel.DataAnnotations;

namespace SmartHouse.Models.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ZipCode { get; set; }
    }
}
