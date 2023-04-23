using loginRegister.Modal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace loginRegister.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
       

        public UserController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment ,IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult GetAllUser()
        {
            return Ok(_context.Users.Include(c => c.City).Include(c => c.City.State).Include(c => c.City.State.Country).ToList());
        }
        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] User users)
        {

            if (users == null) return BadRequest();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == users.UserName || x.Email == users.Email && x.Password == users.Password);
            if (user == null)
                return NotFound();
            return Ok(new
            {
                message = "login succesffulyy"
            });
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
             {
            try
            {
                if (user != null)
                {   
                    var usersExist = await _context.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName);
                    if (usersExist != null)
                        return BadRequest("userName Aready Exist!! ");
                    var emailExist = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
                    if (emailExist != null)
                        return BadRequest("email already exist !!");


                    

                    if (!string.IsNullOrEmpty(user.SiteLogo))
                    {
                        
                        string[] logoParts = user.SiteLogo.Split(',');
                        if (logoParts.Length == 2)
                        {
                            string mimeType = logoParts[0].Split(':')[1];                            
                            string base64Data = logoParts[1];                         
                            byte[] imageBytes = Convert.FromBase64String(base64Data);                         
                            string fileName = Guid.NewGuid().ToString() + ".jpg";                          
                            string filePath = Path.Combine("wwwroot", "images", "logos", fileName);                          
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await fileStream.WriteAsync(imageBytes, 0, imageBytes.Length);
                            }                           
                            user.SiteLogo = @"\images\logos\"+fileName;
                        } 
                    }
                    if (!string.IsNullOrEmpty(user.ProfilePicture))
                    {
                        string[] profileParts = user.ProfilePicture.Split(',');
                        if (profileParts.Length == 2)
                        {                         
                            string mimeType = profileParts[0].Split(':')[1];
                            string base64Data = profileParts[1]; 
                            byte[] imageBytes = Convert.FromBase64String(base64Data);
                            string fileName = Guid.NewGuid().ToString() + ".jpg";                        
                           string filePath = Path.Combine("wwwroot", "images", "profilepicture", fileName);
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await fileStream.WriteAsync(imageBytes, 0, imageBytes.Length);
                            }
                            user.ProfilePicture = @"\images\profilepicture\"+fileName;
                        }
                    }
                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();



                    var subject = "Welcome to MyApp";
                    var message = $"Dear {user.FirstName}, welcome to MyApp!";
                    var callBackurl = Url.Action("", new { userid = user.Id }, protocol:
                        HttpContext.Request.Scheme);
                    await _emailSender.SendEmailAsync(user.Email, subject, message);

                    //<a href=$"{Url}?email={user.Email}">

                    return Ok();
                }
            }
            catch (Exception )
            {

                throw;
            }
            

            return BadRequest();
        }

    }
   


}


