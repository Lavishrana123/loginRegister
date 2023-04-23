using loginRegister.Modal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace loginRegister.Controllers
{
    [Route("api/country")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CountryController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetCountries()
        {
            return Ok(_context.Countries.ToList());
        }
        [HttpPost]
        public IActionResult SaveCountry([FromBody] Country country)
        {
            if (country != null && ModelState.IsValid)
            {
                _context.Countries.Add(country);
                _context.SaveChanges();
                return Ok();
            }
            return CreatedAtRoute("GetCountry", new { id = country.Id }, country);
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteCountry(int id)
        {
            var employeefromdb = _context.Countries.Find(id);
            if (employeefromdb == null) return NotFound();
            _context.Countries.Remove(employeefromdb);
            _context.SaveChanges();
            return Ok();
        }
    }
}
