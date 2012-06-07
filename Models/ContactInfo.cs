using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Zeta.Models
{
    public class Person
    {
        //string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}";
        //strRegex = strRegex + "@\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\";
        //strRegex = strRegex + "@.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        [Required(ErrorMessage = "Required")]
        [StringLength(50, ErrorMessage = "Must be under 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(50, ErrorMessage = "Must be under 50 characters")]
        public string LasstName { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Address { get; set; }

        public string Address2 { get; set; }

        [Required(ErrorMessage = "Required")]
        public string City { get; set; }

        //[RegularExpression("^(\d{5}|[a-zA-Z][0-9][a-zA-Z]( ){0,1}[0-9][a-zA-Z][0-9])$", ErrorMessage = "Not a valid Zip Code")]
        public string Zip { get; set; }

        public string Phone { get; set; }


        //[Required(ErrorMessage = "Required")]
        //[Range(0, 120, ErrorMessage = "Age must be between 0 and 120")]
        //public string Age { get; set; }
        
        //if ((person.Zipcode.Trim().Length > 0) && (!Regex.IsMatch(person.Zipcode, @"^\d{5}$|^\d{5}-\d{4}$")))
        //{
        //    ModelState.AddModelError("Zipcode", "Zipcode is invalid.");
        //}
        //if (!Regex.IsMatch(person.Phone, @"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"))
        //{
        //    ModelState.AddModelError("Phone", "Phone number is invalid.");
        //}
        //if (!Regex.IsMatch(person.Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
        //    }
        [Required(ErrorMessage = "Required")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$", ErrorMessage = "Not a valid email")]
        public string Email { get; set; }
    }

}