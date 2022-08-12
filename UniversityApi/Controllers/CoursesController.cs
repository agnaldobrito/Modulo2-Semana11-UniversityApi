using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Context;
using UniversityApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniversityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private UniversityContext _universityContext;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(UniversityContext context, ILogger<CoursesController> logger)
        {
            _universityContext = context;
            _logger = logger;
        }

        /// <summary>
        /// Busca e retorna uma lista de cursos no banco de dados
        /// </summary>
        /// <returns>Retorna uma lista de cursos</returns>
        /// <reponse code="200">Retorna com sucesso a lista de cursos</reponse>
        /// <response code="404">Não foi encontrada a lista de cursos</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Course>>> Get()
        {
            try
            {
                IEnumerable<Course> courses = await _universityContext.Courses.ToListAsync();

                _logger.LogInformation($"Controller: {nameof(CoursesController)} - Endpoint: {nameof(Get)}");


                return courses.Any() ? Ok(courses) : NotFound();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Controller: {nameof(CoursesController)} - Endpoint: {nameof(Get)}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Busca e retorna um curso especificado por ID no banco de dados
        /// </summary>
        /// <param name="id">ID do Curso</param>
        /// <returns>Retorna o curso especificado</returns>
        /// <reponse code="200">Retorna com sucesso o curso especificado</reponse>
        /// <response code="404">Não foi encontrada o curso especificado</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Course>> GetById(int id)
        {
            try
            {
                Course? course = await _universityContext.Courses.FirstOrDefaultAsync(course => course.Id == id);

                _logger.LogInformation($"Controller: {nameof(CoursesController)} - Endpoint: {nameof(GetById)}");

                return course is not null ? Ok(course) : NotFound();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Controller: {nameof(CoursesController)} - Endpoint: {nameof(GetById)}");

                return StatusCode(500);

            }
        }

        /// <summary>
        /// Adiciona um curso no banco de dados
        /// </summary>
        /// <param name="infoCourse">Informações do curso</param>
        /// <returns>Retorna resposta se o curso foi inserido com sucesso ou não no banco de dados</returns>
        /// <response code="201">O curso foi inserido com sucesso</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] Course infoCourse)
        {
            try
            {
                _universityContext.Courses.Add(infoCourse);
                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Controller: {nameof(CoursesController)} - Endpoint: {nameof(Post)}");

                return CreatedAtAction(nameof(GetById), new { Id = infoCourse.Id }, infoCourse);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Controller: {nameof(CoursesController)} - Endpoint: {nameof(Post)}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Atualiza informações sobre o curso especificado
        /// </summary>
        /// <param name="updateCourse"></param>
        /// <returns>Retorna resposta se a atualização do curso foi um sucesso ou não no banco de dados</returns>
        /// <response code="202">A informação do curso foi alteada com sucesso</response>
        /// <response code="404">Não foi encontrado o curso especificado</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] Course updateCourse)
        {
            try
            {
                bool courseExist = _universityContext.Courses.Any(course => course.Id == updateCourse.Id);

                if (courseExist is false) return NotFound();

                _universityContext.Courses.Update(updateCourse);
                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Controller: {nameof(CoursesController)} - Endpoint: {nameof(Put)}");

                return Accepted();


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Controller: {nameof(CoursesController)} - Endpoint: {nameof(Put)}");
                return StatusCode(500);

            }
        }

        /// <summary>
        /// Remove o curso do banco de dados
        /// </summary>
        /// <param name="id">ID do curso</param>
        /// <returns>Retorna se a remoção do curso no banco de dados foi um sucesso ou não</returns>
        /// <response code="204">Curso removido com sucesso</response>
        /// <response code="404">Curso não encontrado</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Course? course = await _universityContext.Courses.FirstOrDefaultAsync(course => course.Id == id);

                if (course is null) return NotFound();

                _logger.LogInformation($"Controller: {nameof(CoursesController)} - Endpoint: {nameof(Delete)}");

                _universityContext.Courses.Remove(course);
                await _universityContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Controller: {nameof(CoursesController)} - Endpoint: {nameof(Delete)}");
                return StatusCode(500);

            }
        }
    }
}
