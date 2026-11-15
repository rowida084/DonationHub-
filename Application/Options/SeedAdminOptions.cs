using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Options
{
   public class SeedAdminOptions
    {
        public const string SectionName = "SeedAdmin";
        [Required]
        [EmailAddress]
        public string? Email {  get; set; }
        [Required]
        [MinLength(8)]
        public string? Password { get; set; }
        [Required]
        public string? FullName { get; set; }
    }
}
