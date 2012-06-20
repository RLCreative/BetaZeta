using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Zeta.Models
{
    public class DropDownViewModel
    {

        public SelectList StateList { get; set; }
    }
}