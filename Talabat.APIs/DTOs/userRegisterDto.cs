﻿using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs
{
    public class userRegisterDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
