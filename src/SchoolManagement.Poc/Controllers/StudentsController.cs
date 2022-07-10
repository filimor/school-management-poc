using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Poc.Data;
using SchoolManagement.Poc.Models;

namespace SchoolManagement.Poc.Controllers;

[ApiController]
[Route("[Controller]")]
public class StudentsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public StudentsController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAllStudents()
    {
        var students = _context?.Students?.Include(a => a.Address).ToList();
        var studentsDto = _mapper.Map<List<GetStudentDto>>(students);
        return Ok(studentsDto);
    }

    [HttpGet("{id}")]
    public IActionResult GetStudentById(int id)
    {
        var student = _context?.Students?.Include(a => a.Address).SingleOrDefault(student => student.Id == id);

        if (student == null) return NotFound();

        var studentDto = _mapper.Map<GetStudentDto>(student);
        return Ok(studentDto);
    }

    [HttpPost]
    public IActionResult CreateStudent([FromBody] WriteStudentDto writeStudentDto)
    {
        var student = _mapper.Map<Student>(writeStudentDto);
        _context?.Students?.Add(student);
        _context?.SaveChanges();

        return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateStudent(int id, [FromBody] WriteStudentDto writeStudentDto)
    {
        var student = _context?.Students?.FirstOrDefault(student => student.Id == id);

        if (student == null) return NotFound();

        _mapper.Map(writeStudentDto, student);
        _context?.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteStudent(int id)
    {
        var student = _context?.Students?.FirstOrDefault(student => student.Id == id);

        if (student == null) return NotFound();

        _context?.Students?.Remove(student);
        _context?.SaveChanges();

        return NoContent();
    }
}