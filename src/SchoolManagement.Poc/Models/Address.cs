using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Poc.Models
{
    public class Address
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string District { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string ZipCode { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
