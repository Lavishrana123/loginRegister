using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace loginRegister.Modal
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public int PostalCode { get; set; }
        public string Address { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }       
        public string SiteLogo { get; set; }
        public string ProfilePicture { get; set; }
        [Display(Name = "City")]
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }
        public bool emailConfirmed { get; set; }
        //  public string EmailConfirmed { get; set; }
        //  public string EmailConfirmedToken { get; set; }


        //[NotMapped]
        //[Display(Name = "State")]
        //public int Stateid { get; set; }
        //[NotMapped]
        //[Display(Name = "Country")]
        //public int Countryid { get; set; }

    }
}
