﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Login.Models
{
    public class AccountRegisterVM
    {
        [Required(ErrorMessage = "First name is required!")]
        [MinLength(2, ErrorMessage = "First name should be at least 2 characters long!")]
        [MaxLength(255, ErrorMessage = "First name should be at most 255 characters long!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required!")]
        [MinLength(2, ErrorMessage = "Last name should be at least 2 characters long!")]
        [MaxLength(255, ErrorMessage = "Last name should be at most 255 characters long!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [MinLength(4, ErrorMessage = "Password should be at least 2 characters long!")]
        [MaxLength(255, ErrorMessage = "Password should be at most 255 characters long!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [ValidateNever]
        public string ErrorMessage { get; set; }
    }
}