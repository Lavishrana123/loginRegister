using loginRegister.Modal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace loginRegister.Controllers
{
    [Route("api/city")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CityController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet(Name = "GetCity")]
        public IActionResult GetCities()
        {
            var getCity = _context.Cities.Include(c => c.State).Include(c => c.State.Country).ToList();
            return Ok(getCity);
        }
        [HttpPost]
        public IActionResult SaveCity([FromBody] City city)
        {
            if (city != null && ModelState.IsValid)
            {
                _context.Cities.Add(city);
                 _context.SaveChanges();
                return CreatedAtRoute("GetCity", new { id = city.Id }, city);
            }
            return NotFound();
        }
        [HttpGet("{stateId}", Name = "LoadCityByStateId")]
        public IActionResult LoadCityByStateId(int stateId)
        {
            var cities = _context.Cities.Where(s => s.StateId == stateId).Select(s => new
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();
            return Ok(cities);
          
        }
    }
}

