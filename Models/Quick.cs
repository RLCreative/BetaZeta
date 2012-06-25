using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Zeta.Models
{
    public class Quick
    {        
        //string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}";
        //strRegex = strRegex + "@\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\";
        //strRegex = strRegex + "@.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        [Required(ErrorMessage = "Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must have a minimum length of 3.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must have a minimum length of 3.")]
        public string LastName { get; set; }

        [StringLength(12)]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^\s*([\(]?)\[?\s*\d{3}\s*\]?[\)]?\s*[\-]?[\.]?\s*\d{3}\s*[\-]?[\.]?\s*\d{4}$", ErrorMessage = "Invalid Phone")]
        public string Phone { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Not a valid email")] //US ZIP  
        public string Email { get; set; }

        
        //public string Upload { get; set; }

        [RegularExpression(@"^.*\.(jpg|JPG|)$", ErrorMessage = "Not a valid file")]
        public HttpPostedFileBase Attachment { get; set; }
    }
}
