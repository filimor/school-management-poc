using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Poc.Data;
using SchoolManagement.Poc.Models;

namespace SchoolManagement.Poc.Controllers;

[ApiController]
[Route("[Controller]")]
[Produces("application/json")]
[ApiConventionType(typeof(DefaultApiConventions))]
public class StudentsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public StudentsController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Obtém todos os alunos cadastrados.
    /// </summary>
    /// <remarks>Retorna uma lista com todos os alunos cadastrados.</remarks>
    /// <returns>Um array com os alunos.</returns>
    [HttpGet]
    public IActionResult GetAllStudents()
    {
        var students = _context?.Students?.Include(a => a.Address).ToList();
        var studentsDto = _mapper.Map<List<GetStudentDto>>(students);
        return Ok(studentsDto);
    }

    /// <summary>
    /// Obtém os dados de um aluno por id.
    /// </summary>
    /// <remarks>Retorna os dados de um aluno com base no id especificado.</remarks>
    /// <param name="id">Id do aluno.</param>
    [HttpGet("{id}")]
    public IActionResult GetStudentById(int id)
    {
        var student = _context?.Students?.Include(a => a.Address).SingleOrDefault(student => student.Id == id);

        if (student == null) return NotFound();

        var studentDto = _mapper.Map<GetStudentDto>(student);
        return Ok(studentDto);
    }

    /// <summary> Cadastra um aluno.</summary>
    /// <remarks>
    /// É necessário informar um id de endereço válido e não utilizado.
    ///
    /// Exemplo de requisição:
    ///
    ///     POST /addresses
    ///     {
    ///         "name": "João da Silva",
    ///         "phone": "11999998888",
    ///         "email": "joao@silva.com",
    ///         "addressId": 1
    ///     }
    /// </remarks>
    /// <returns>O aluno cadastrado.</returns>
    /// <remarks>Retorna o aluno cadastrado.</remarks>
    [HttpPost]
    public IActionResult CreateStudent([FromBody] WriteStudentDto writeStudentDto)
    {
        var student = _mapper.Map<Student>(writeStudentDto);
        _context?.Students?.Add(student);
        _context?.SaveChanges();

        return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
    }

    /// <summary>
    /// Atualiza um aluno.
    /// </summary>
    /// <remarks>Atualiza o aluno correspondente ao ID recebido como parâmetro utilizando
    /// os dados recebidos no corpo da requisição.</remarks>
    /// <param name="id">Id do aluno a ser alterado.</param>
    [HttpPut("{id}")]
    public IActionResult UpdateStudent(int id, [FromBody] WriteStudentDto writeStudentDto)
    {
        var student = _context?.Students?.FirstOrDefault(student => student.Id == id);

        if (student == null) return NotFound();

        _mapper.Map(writeStudentDto, student);
        _context?.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Exclui um aluno.
    /// </summary>
    /// <remarks>Exclui o aluno correspondente ao ID recebido.</remarks>
    /// <param name="id">Id do aluno a ser excluído.</param>
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