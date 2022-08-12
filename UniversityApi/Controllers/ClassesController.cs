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
        public ClassesController(UniversityContext universityContext)
        {
            _universityContext = universityContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> Get()
        {
            try
            {
                IEnumerable<Class> classes = await _universityContext.Classes
                    .Include(x => x.Course)
                    .Include(y => y.Course)
                    .ToListAsync();

                return classes.Any() ? Ok(classes) : StatusCode(404);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> GetById(int id)
        {
            try
            {
                Class class1 = await _universityContext.Classes
                    .Include(x => x.Course)
                    .Include(y => y.Instructor)
                    .FirstOrDefaultAsync(x => x.Id == id);

                return class1 is not null ? Ok(class1) : StatusCode(404);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Class class1)
        {
            try
            {
                _universityContext.Classes.Add(class1);
                await _universityContext.SaveChangesAsync();

                return CreatedAtAction("GetById", new { Id = class1.Id }, class1);

            }
            catch
            {
                return StatusCode(500);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Class class1)
        {
            try
            {
                bool classExist = _universityContext.Classes.Any(x => x.Id == class1.Id);
                if (!classExist) return NotFound();

                _universityContext.Entry(class1).State = EntityState.Modified;
                await _universityContext.SaveChangesAsync();

                return StatusCode(202);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var class1 = await _universityContext.Classes.FindAsync(id);

                if (class1 is null) return NotFound();

                _universityContext.Classes.Remove(class1);
                await _universityContext.SaveChangesAsync();

                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
