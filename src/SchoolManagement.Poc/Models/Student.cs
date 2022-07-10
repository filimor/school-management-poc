using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Poc.Models
{
    public class Student
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public Address Address { get; set; }
    }
}
