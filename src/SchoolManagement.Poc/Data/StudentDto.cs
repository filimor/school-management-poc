using SchoolManagement.Poc.Models;

namespace SchoolManagement.Poc.Data;

public class StudentDto
{
    public string Name { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public Address Address { get; set; }
}