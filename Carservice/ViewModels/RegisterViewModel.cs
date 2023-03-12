﻿using System.ComponentModel.DataAnnotations;

namespace Carservice.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; } = "";

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = "";

        [Required]
        public DateTime DateOfBirthday { get; set; } 
    }
}
