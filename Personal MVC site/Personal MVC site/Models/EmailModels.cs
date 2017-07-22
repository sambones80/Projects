﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Personal_MVC_site.Models
{
    public class EmailModel
    {
        [Required, Display(Name = "Name")]
        public string ToName { get; set; }
        [Required, Display(Name = "Email"), EmailAddress]
        public string ToEmail { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
    }
}