using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Context;
using UniversityApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniversityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private UniversityContext _universityContext;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(UniversityContext context,ILogger<StudentsController> logger)
        {
            _universityContext = context;
            _logger = logger;   
        }

        /// <summary>
        /// Busca e retorna uma lista de alunos no banco de dados
        /// </summary>
        /// <returns>Retorna uma lista de alunos</returns>
        /// <reponse code="200">Retorna com sucesso a lista de alunos</reponse>
        /// <response code="404">Não foi encontrada a lista de alunos</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Student>>> Get()
        {
            try
            {
                IEnumerable<Student> students = await _universityContext.Students.ToListAsync();

                _logger.LogInformation($"Controller: {nameof(StudentsController)} - Endpoint: {nameof(Get)}");


                return students.Any() ? Ok(students) : NotFound();

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Controller: {nameof(StudentsController)} - Endpoint: {nameof(Get)}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Busca e retorna um aluno especificado por ID no banco de dados
        /// </summary>
        /// <param name="id">ID do Aluno</param>
        /// <returns>Retorna o aluno especificado</returns>
        /// <reponse code="200">Retorna com sucesso o aluno especificado</reponse>
        /// <response code="404">Não foi encontrada o aluno especificado</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Student>> GetById(int id)
        {
            try
            {
                Student? student = await _universityContext.Students.FirstOrDefaultAsync(student => student.Id == id);

                _logger.LogInformation($"Controller: {nameof(StudentsController)} - Endpoint: {nameof(GetById)}");

                return student is not null ? Ok(student) : NotFound();

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Controller: {nameof(StudentsController)} - Endpoint: {nameof(GetById)}");
                
                return StatusCode(500);

            }
        }

        /// <summary>
        /// Adiciona um aluno no banco de dados
        /// </summary>
        /// <param name="infoStudent">Informações do aluno</param>
        /// <returns>Retorna resposta se o aluno foi inserido com sucesso ou não no banco de dados</returns>
        /// <response code="201">O aluno foi inserido com sucesso</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] Student infoStudent)
        {
            try
            {
                _universityContext.Students.Add(infoStudent);
                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Controller: {nameof(StudentsController)} - Endpoint: {nameof(Post)}");

                return CreatedAtAction(nameof(GetById),new { Id = infoStudent.Id},infoStudent);


            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Controller: {nameof(StudentsController)} - Endpoint: {nameof(Post)}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Atualiza informações sobre o aluno especificado
        /// </summary>
        /// <param name="updateStudent"></param>
        /// <returns>Retorna resposta se a atualização do aluno foi um sucesso ou não no banco de dados</returns>
        /// <response code="202">A informação do aluno foi alteada com sucesso</response>
        /// <response code="404">Não foi encontrado o aluno especificado</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] Student updateStudent)
        {
            try
            {
                bool studentExist = _universityContext.Students.Any(student => student.Id == updateStudent.Id);

                if (studentExist is false) return NotFound();

                _universityContext.Students.Update(updateStudent);
                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Controller: {nameof(StudentsController)} - Endpoint: {nameof(Put)}");

                return Accepted();


            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Controller: {nameof(StudentsController)} - Endpoint: {nameof(Put)}");
                return StatusCode(500);

            }
        }

        /// <summary>
        /// Remove o aluno do banco de dados
        /// </summary>
        /// <param name="id">ID do aluno</param>
        /// <returns>Retorna se a remoção do aluno no banco de dados foi um sucesso ou não</returns>
        /// <response code="204">Aluno removido com sucesso</response>
        /// <response code="404">Aluno não encontrado</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Student? student =  await _universityContext.Students.FirstOrDefaultAsync(student => student.Id == id);

                if (student is null) return NotFound();

                _logger.LogInformation($"Controller: {nameof(StudentsController)} - Endpoint: {nameof(Delete)}");

                _universityContext.Students.Remove(student);
                await _universityContext.SaveChangesAsync();

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Controller: {nameof(StudentsController)} - Endpoint: {nameof(Delete)}");
                return StatusCode(500);

            }
        }
    }
}
