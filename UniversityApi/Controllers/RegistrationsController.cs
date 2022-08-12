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
        public RegistrationsController(UniversityContext universityContext)
        {
            _universityContext = universityContext;
        }
        // GET: api/<RegistrationController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var registrations = await _universityContext.Registrations
                    .Include(x => x.Class)
                    .Include(z => z.Class.Instructor)
                    .Include(w => w.Class.Course)
                    .Include(y => y.Student)
                    .ToListAsync();

                return registrations.Any() ? Ok(registrations) : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // GET api/<RegistrationController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var registrations = await _universityContext.Registrations
                    .Include(x => x.Class)
                    .Include(z => z.Class.Instructor)
                    .Include(w => w.Class.Course)
                    .Include(y => y.Student)
                    .FirstOrDefaultAsync();

                return registrations is not null ? Ok(registrations) : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // POST api/<RegistrationController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Registration registration)
        {
            try
            {
                _universityContext.Registrations.Add(registration);
                await _universityContext.SaveChangesAsync();

                return CreatedAtAction("GetById", new { Id = registration.Id }, registration);

            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        // PUT api/<RegistrationController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Registration registration)
        {
            try
            {
                bool registrationExist = _universityContext.Registrations.Any(x => x.Id == registration.Id);
                if (!registrationExist) return NotFound();

                _universityContext.Entry(registration).State = EntityState.Modified;
                await _universityContext.SaveChangesAsync();

                return StatusCode(202);
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        // DELETE api/<RegistrationController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var registration = await _universityContext.Registrations.FindAsync(id);

                if (registration is null) return NotFound();

                _universityContext.Registrations.Remove(registration);
                await _universityContext.SaveChangesAsync();

                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
