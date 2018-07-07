﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memberships.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserViewModel
    {
        [Display(Name = "User Id")]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name="First Name")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {1} characters long", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}