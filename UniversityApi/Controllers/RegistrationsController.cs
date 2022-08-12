using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Context;
using UniversityApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniversityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {
        private UniversityContext _universityContext;
        private readonly ILogger<RegistrationsController> _logger;
        public RegistrationsController(UniversityContext universityContext, ILogger<RegistrationsController> logger)
        {
            _universityContext = universityContext;
            _logger = logger;
        }

        /// <summary>
        /// Busca e retorna uma lista de matriculas no banco de dados
        /// </summary>
        /// <returns>Retorna uma lista de matriculas</returns>
        /// <reponse code="200">Retorna com sucesso a lista de matriculas</reponse>
        /// <response code="404">Não foi encontrada a lista de matriculas</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Registration>>> Get()
        {
            try
            {
                IEnumerable<Registration> registrations = await _universityContext.Registrations
                    .Include(x => x.Class)
                    .Include(z => z.Class.Instructor)
                    .Include(w => w.Class.Course)
                    .Include(y => y.Student)
                    .ToListAsync();

                _logger.LogInformation($"Controller: {nameof(RegistrationsController)} - Endpoint: {nameof(Get)}");

                return registrations.Any() ? Ok(registrations) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Controller: {nameof(RegistrationsController)} - Endpoint: {nameof(Get)}");
                return StatusCode(500);
            }
        }


        /// <summary>
        /// Busca e retorna uma matricula especificada por ID no banco de dados
        /// </summary>
        /// <param name="id">ID da Matricula</param>
        /// <returns>Retorna a matricula especificada</returns>
        /// <reponse code="200">Retorna com sucesso a matricula especificada</reponse>
        /// <response code="404">Não foi encontrada a matricula especificada</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Registration>> GetById(int id)
        {
            try
            {
                Registration? registrations = await _universityContext.Registrations
                    .Include(x => x.Class)
                    .Include(z => z.Class.Instructor)
                    .Include(w => w.Class.Course)
                    .Include(y => y.Student)
                    .FirstOrDefaultAsync();

                _logger.LogInformation($"Controller: {nameof(RegistrationsController)} - Endpoint: {nameof(GetById)}");

                return registrations is not null ? Ok(registrations) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Controller: {nameof(RegistrationsController)} - Endpoint: {nameof(GetById)}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Adiciona uma matricula no banco de dados
        /// </summary>
        /// <param name="infoRegistration">Informações da matricula</param>
        /// <returns>Retorna resposta se a matricula foi inserida com sucesso ou não no banco de dados</returns>
        /// <response code="201">A matricula foi inserida com sucesso</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] Registration infoRegistration)
        {
            try
            {
                _universityContext.Registrations.Add(infoRegistration);
                await _universityContext.SaveChangesAsync();

                return CreatedAtAction("GetById", new { Id = infoRegistration.Id }, infoRegistration);

            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Atualiza informações da matricula especificada
        /// </summary>
        /// <param name="updateRegistration"></param>
        /// <returns>Retorna resposta se a atualização da matricula foi um sucesso ou não no banco de dados</returns>
        /// <response code="202">A informação da matricula foi alteada com sucesso</response>
        /// <response code="404">Não foi encontrado a matricula especificada</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] Registration updateRegistration)
        {
            try
            {
                bool registrationExist = _universityContext.Registrations.Any(x => x.Id == updateRegistration.Id);

                if (registrationExist is false) return NotFound();

                _universityContext.Entry(updateRegistration).State = EntityState.Modified;
                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Controller: {nameof(RegistrationsController)} - Endpoint: {nameof(Put)}");

                return StatusCode(202);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Controller: {nameof(RegistrationsController)} - Endpoint: {nameof(Put)}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Remove a matricula do banco de dados
        /// </summary>
        /// <param name="id">ID da matricula</param>
        /// <returns>Retorna se a remoção da matricula no banco de dados foi um sucesso ou não</returns>
        /// <response code="204">Matricula removida com sucesso</response>
        /// <response code="404">Matricula não encontrada</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var registration = await _universityContext.Registrations.FindAsync(id);

                if (registration is null) return NotFound();

                _universityContext.Registrations.Remove(registration);
                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Controller: {nameof(RegistrationsController)} - Endpoint: {nameof(Delete)}");

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Controller: {nameof(RegistrationsController)} - Endpoint: {nameof(Delete)}");
                return StatusCode(500);
            }
        }
    }
}
