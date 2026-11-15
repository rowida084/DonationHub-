using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Options
{
    public class JwtOptions
    {
        public const string SectionName = "Jwt";

        [Required]
        [MinLength(32)]
        public string? SecretKey { get; set; }

        [Required]
        public string? Issuer { get; set; }

        [Required]
        public string? Audience {  get; set; }

        [Range(1, 1440)]
        public int ExpiryMinutes {  get; set; }

        [Range(1,365)]
        public int RefreshTokenExpiryDays {  get; set; }
    }
}
