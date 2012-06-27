using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Zeta.Models
{
    public class Media
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

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must have a minimum length of 3.")]
        [Required(ErrorMessage = "Required")]
        public string Company { get; set; }
        
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must have a minimum length of 3.")]
        [Required(ErrorMessage = "Required")]
        public string Address { get; set; }

        [StringLength(50)]
        public string Address2 { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must have a minimum length of 3.")]
        [Required(ErrorMessage = "Required")]
        public string City { get; set; }

        [StringLength(2, MinimumLength = 2, ErrorMessage = "You must select a state.")]
        [Required(ErrorMessage = "Required")]
        public string State { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Not a valid ZIP Code")] //US ZIP 
        public string Zip { get; set; }

        [StringLength(2, MinimumLength = 2, ErrorMessage = "You must select a country.")]
        [Required(ErrorMessage = "Required")]
        public string Country { get; set; }

        [StringLength(12)]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^\s*([\(]?)\[?\s*\d{3}\s*\]?[\)]?\s*[\-]?[\.]?\s*\d{3}\s*[\-]?[\.]?\s*\d{4}$", ErrorMessage = "Invalid Phone")]
        public string Phone { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Not a valid email")] 
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must have a minimum length of 3.")]
        public string whatBrands { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must have a minimum length of 3.")]
        public string ContactReason { get; set; }

        [Required(ErrorMessage = "Required")]        
        public bool Blogger { get; set; }

        // Blogger Specific

        [Required(ErrorMessage = "Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must have a minimum length of 3.")]
        public string YearStarted { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must have a minimum length of 3.")]
        public string WebURL { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must have a minimum length of 3.")]
        public string MonthlyVisitors { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must have a minimum length of 3.")]
        public string UniqueVisitors { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must have a minimum length of 3.")]
        public string PageViews { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must have a minimum length of 3.")]
        public string PageRanking { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must have a minimum length of 3.")]
        public string Facebook { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must have a minimum length of 3.")]
        public string Twitter { get; set; }

        //[Required(ErrorMessage = "Required")]
        //[StringLength(50, MinimumLength = 3, ErrorMessage = "Must have a minimum length of 3.")]
        //public string Testers { get; set; }


    }
}
