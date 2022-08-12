using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Models;
using UniversityApi.Context;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace University2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private UniversityContext _universityContext;
        private readonly ILogger<ClassesController> _logger;

        public ClassesController(UniversityContext universityContext, ILogger<ClassesController> logger)
        {
            _universityContext = universityContext;
            _logger = logger;
        }

        /// <summary>
        /// Busca e retorna uma lista de turmas no banco de dados
        /// </summary>
        /// <returns>Retorna uma lista de turmas</returns>
        /// <reponse code="200">Retorna com sucesso a lista de turmas</reponse>
        /// <response code="404">Não foi encontrada a lista de turmas</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Class>>> Get()
        {
            try
            {
                IEnumerable<Class> classes = await _universityContext.Classes
                    .Include(x => x.Instructor)
                    .Include(y => y.Course)
                    .ToListAsync();

                _logger.LogInformation($"Controller: {nameof(ClassesController)} - Endpoint: {nameof(Get)}");


                return classes.Any() ? Ok(classes) : StatusCode(404);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,$"Controller: {nameof(ClassesController)} - Endpoint: {nameof(Get)}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Busca e retorna uma turma especificada por ID no banco de dados
        /// </summary>
        /// <param name="id">ID da Turma</param>
        /// <returns>Retorna a turma especificada</returns>
        /// <reponse code="200">Retorna com sucesso a turma especificada</reponse>
        /// <response code="404">Não foi encontrada a turma especificada</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Class>> GetById(int id)
        {
            try
            {
                Class? class1 = await _universityContext.Classes
                    .Include(x => x.Instructor)
                    .Include(y => y.Course)
                    .FirstOrDefaultAsync(x => x.Id == id);

                _logger.LogInformation($"Controller: {nameof(ClassesController)} - Endpoint: {nameof(GetById)}");


                return class1 is not null ? Ok(class1) : StatusCode(404);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Controller: {nameof(ClassesController)} - Endpoint: {nameof(GetById)}");

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Adiciona uma turma no banco de dados
        /// </summary>
        /// <param name="infoClass">Informações da turma</param>
        /// <returns>Retorna resposta se a turma foi inserida com sucesso ou não no banco de dados</returns>
        /// <response code="201">A turma foi inserida com sucesso</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] Class infoClass)
        {
            try
            {
                _universityContext.Classes.Add(infoClass);
                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Controller: {nameof(ClassesController)} - Endpoint: {nameof(Post)}");


                return CreatedAtAction("GetById", new { Id = infoClass.Id }, infoClass);

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Controller: {nameof(ClassesController)} - Endpoint: {nameof(Post)}");

                return StatusCode(500);
            }

        }

        /// <summary>
        /// Atualiza informações da turma especificada
        /// </summary>
        /// <param name="updateClass"></param>
        /// <returns>Retorna resposta se a atualização da turma foi um sucesso ou não no banco de dados</returns>
        /// <response code="202">A informação da turma foi alteada com sucesso</response>
        /// <response code="404">Não foi encontrado a matricula especificada</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] Class updateClass)
        {
            try
            {
                bool classExist = _universityContext.Classes.Any(x => x.Id == updateClass.Id);
                if (!classExist) return NotFound();

                _universityContext.Entry(updateClass).State = EntityState.Modified;
                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Controller: {nameof(ClassesController)} - Endpoint: {nameof(Put)}");


                return StatusCode(202);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Controller: {nameof(ClassesController)} - Endpoint: {nameof(Put)}");

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Remove a turma do banco de dados
        /// </summary>
        /// <param name="id">ID da turma</param>
        /// <returns>Retorna se a remoção da turma no banco de dados foi um sucesso ou não</returns>
        /// <response code="204">Turma removida com sucesso</response>
        /// <response code="404">Turma não encontrada</response>
        /// <response code="500">Ocorreu erro durante a execução</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var class1 = await _universityContext.Classes.FindAsync(id);

                if (class1 is null) return NotFound();

                _universityContext.Classes.Remove(class1);
                await _universityContext.SaveChangesAsync();

                _logger.LogInformation($"Controller: {nameof(ClassesController)} - Endpoint: {nameof(Delete)}");


                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Controller: {nameof(ClassesController)} - Endpoint: {nameof(Delete)}");

                return StatusCode(500);
            }
        }
    }
}
