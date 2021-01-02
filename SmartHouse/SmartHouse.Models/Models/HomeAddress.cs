using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHouse.Models.Models
{
    public class HomeAddress
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Street { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(City))]
        public int CityId { get; set; }
        public City City { get; set; }

        public bool Active { get; set; }
    }
}
