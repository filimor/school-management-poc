using SchoolManagement.Poc.Models;

namespace SchoolManagement.Poc.Data;

public class GetStudentDto
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public int AddressId { get; set; }
    public Address Address { get; set; }
}