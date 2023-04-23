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
    [Route("api/state")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StateController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet(Name = "GetState")]
        public IActionResult GetStates()
        {
            var getAllStates = _context.States.Include(s => s.Country).ToList();

            return Ok(getAllStates);
        }
        [HttpPost]
        public IActionResult SaveState([FromBody] State state)
        {
            if (state != null && ModelState.IsValid)
            {
                _context.States.Add(state);
                _context.SaveChanges();
                return CreatedAtRoute("GetState", new { id = state.Id }, state);
            }
            return NotFound();
            
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteState(int id)
        {
            var employeefromdb = _context.States.Find(id);
            if (employeefromdb == null) return NotFound();
            _context.States.Remove(employeefromdb);
            _context.SaveChanges();
            return Ok();
        }
        //[HttpGet("{countryId}", Name = "LoadStateByCountryId")]
        //public IActionResult LoadStateByCountryId(int countryId)
        //{
        //    var a = _context.States.Where(s => s.CountryId == countryId).ToList();
        //    var stateListData = a.Select(sl => new SelectListItem()
        //    {
        //        Text = sl.Name,
        //        Value = sl.Id.ToString()
        //    }).ToList();
        //    return Ok(stateListData);
        //}
        [HttpGet("{countryId}", Name = "LoadStateByCountryId")]
        public IActionResult LoadStateByCountryId(int countryId)
        {
            var states = _context.States
                .Where(s => s.CountryId == countryId)
                .Select(s => new
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .ToList();

            return Ok(states);
        }
    }
}
