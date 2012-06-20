using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mail;
using System.ComponentModel.DataAnnotations;


namespace Zeta.Models
{
    public class AddAttachment
    {
        [Required]
        public string Question { get; set; }

        public HttpPostedFileBase Attachment { get; set; }

    }
}