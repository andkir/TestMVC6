﻿using System.ComponentModel.DataAnnotations;

namespace theworld50.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        [StringLength(255, MinimumLength =5)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(255, MinimumLength =5)]
        public string Message { get; set; }
    }
}
