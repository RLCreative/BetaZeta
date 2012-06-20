using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Zeta.Models
{
    public class Person
    {

        //string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}";
        //strRegex = strRegex + "@\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\";
        //strRegex = strRegex + "@.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        [Required(ErrorMessage = "Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must have a minimum length of 3.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must have a minimum length of 3.")]
        public string LasstName { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must have a minimum length of 3.")]
        [Required(ErrorMessage = "Required")]
        public string Address { get; set; }

        [StringLength(50)]
        public string Address2 { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must have a minimum length of 3.")]
        [Required(ErrorMessage = "Required")]
        public string City { get; set; }

        //public SelectList StateList { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "Must have a minimum length of 2.")]
        public string State { get; set; }

        //Add Country validate this way, else not required
        //[RegularExpression("^({5}|[a-zA-Z][0-9][a-zA-Z]( ){0,1}[0-9][a-zA-Z][0-9])$", ErrorMessage = "Not a valid Zip Code")] 

        [StringLength(10)]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Not a valid ZIP Code")] //US ZIP 
        public string Zip { get; set; }

        [StringLength(12)]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^\s*([\(]?)\[?\s*\d{3}\s*\]?[\)]?\s*[\-]?[\.]?\s*\d{3}\s*[\-]?[\.]?\s*\d{4}$", ErrorMessage = "Invalid Phone")]
        public string Phone { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Not a valid email")] //US ZIP  
        public string Email { get; set; }

        [RegularExpression(@"^.*\.(jpg|JPG|)$", ErrorMessage = "Not a valid file")]
        public string Upload { get; set; }

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
    }
}
