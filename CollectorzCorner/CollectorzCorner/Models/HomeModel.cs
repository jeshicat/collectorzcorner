using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CollectorzCorner.Models
{
    public class HomeModel {

    }

    public class ContactusModel {

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail address.")]
        [Required(ErrorMessage="Your email is required to submit this form")]
        [Display(Name = "Your Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Comments")]
        [StringLength(1000,MinimumLength = 10)]
        public string Comments { get; set; }
    
    }
}
