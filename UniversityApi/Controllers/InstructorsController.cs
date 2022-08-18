using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Context;
using UniversityApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniversityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private UniversityContext _universityContext;
        private readonly ILogger<StudentsController> _logger;

        public InstructorsController(UniversityContext context, ILogger<StudentsController> logger)
        {
            _universityContext = context;
            _logger = logger;
        }


        /// <summary>
        /// Busca e retorna uma lista de instrutores no banco de dados
        /// </summary>
        /// <returns>Retorna uma lista de instrutores</returns>
        /// <reponse code="200">Retorna com sucesso a lista de instrutores</reponse>
        /// <response code="404">Não foi encontrada a lista de instrutores</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Instructor>>> Get()
        {
            try
            {
                IEnumerable<Instructor> instructors = await _universityContext.Instructors.ToListAsync();

                _logger.LogInformation($"Controller: {nameof(InstructorsController)} - Endpoint: {nameof(Get)}");


                return instructors.Any() ? Ok(instructors) : NotFound();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Controller: {nameof(InstructorsController)} - Endpoint: {nameof(Get)}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Busca e retorna um instrutor especificado por ID no banco de dados
        /// </summary>
        /// <param name="id">ID do Instrutor</param>
        /// <returns>Retorna o instrutor especificado</returns>
        /// <reponse code="200">Retorna com sucesso o instrutor especificado</reponse>
        /// <response code="404">Não foi encontrada o instrutor especificado</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Instructor>> GetById(int id)
        {
            try
            {
                Instructor? instructor = await _universityContext.Instructors.FirstOrDefaultAsync(instructor => instructor.Id == id);

                _logger.LogInformation($"Controller: {nameof(InstructorsController)} - Endpoint: {nameof(GetById)}");

                return instructor is not null ? Ok(instructor) : NotFound();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Controller: {nameof(InstructorsController)} - Endpoint: {nameof(GetById)}");

                return StatusCode(500);

            }
        }

        /// <summary>
        /// Adiciona um instrutor no banco de dados
        /// </summary>
        /// <param name="infoInstructor">Informações do instrutor</param>
        /// <returns>Retorna resposta se o instrutor foi inserido com sucesso ou não no banco de dados</returns>
        /// <response code="201">O instrutor foi inserido com sucesso</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] Instructor infoInstructor)
        {
            try
            {
                _universityContext.Instructors.Add(infoInstructor);
                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Controller: {nameof(InstructorsController)} - Endpoint: {nameof(Post)}");

                return CreatedAtAction(nameof(GetById), new { Id = infoInstructor.Id }, infoInstructor);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Controller: {nameof(InstructorsController)} - Endpoint: {nameof(GetById)}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Atualiza informações sobre o instrutor especificado
        /// </summary>
        /// <param name="updateInstructor"></param>
        /// <returns>Retorna resposta se a atualização do instrutor foi um sucesso ou não no banco de dados</returns>
        /// <response code="202">A informação do instrutor foi alteada com sucesso</response>
        /// <response code="404">Não foi encontrado o instrutor especificado</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] Instructor updateInstructor)
        {
            try
            {
                bool instructorExist = _universityContext.Instructors.Any(instructor => instructor.Id == updateInstructor.Id);

                if (instructorExist is false) return NotFound();

                _universityContext.Instructors.Update(updateInstructor);
                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Controller: {nameof(InstructorsController)} - Endpoint: {nameof(Put)}");

                return Accepted();


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Controller: {nameof(InstructorsController)} - Endpoint: {nameof(Put)}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Remove o instrutor do banco de dados
        /// </summary>
        /// <param name="id">ID do instrutor</param>
        /// <returns>Retorna se a remoção do instrutor no banco de dados foi um sucesso ou não</returns>
        /// <response code="204">Instrutor removido com sucesso</response>
        /// <response code="404">Instrutor não encontrado</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Instructor? instructor = await _universityContext.Instructors.FirstOrDefaultAsync(instrutor => instrutor.Id == id);

                if (instructor is null) return NotFound();

                _logger.LogInformation($"Controller: {nameof(InstructorsController)} - Endpoint: {nameof(Delete)}");

                _universityContext.Instructors.Remove(instructor);
                await _universityContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Controller: {nameof(InstructorsController)} - Endpoint: {nameof(Delete)}");
                return StatusCode(500);

            }
        }
    }   
}
