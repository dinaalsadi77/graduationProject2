using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Graduation_Project2.Models.SharedClass
{
    public class User
    {
       // [DataType(dataType: EmailAddress)]
        public string  EmailAddress { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public long PhoneNumber { get; set; }
        public int Age { get; set; }
       // public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Nationality { get; set; }
        public string Cetegory { get; set; }


        [Display(Name = "ID Photo PH")]
        public string IDPhoto { get; set; }
        
        [NotMapped]
        [Display(Name = "ID Photo File")]
        public IFormFile IdPhoto_File { get; set; }

    }
}
