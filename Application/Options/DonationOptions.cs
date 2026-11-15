using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Options
{
    public class DonationOptions
    {
        public const string SectionName = "Donation";


        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string DefaultCurrency { get; set; } = "USD";

        [Range(typeof(decimal),"1.00", "1000000")]
        public decimal MinAmount { get; set; } = 1.00m;

        [Range(typeof(decimal), "1.00", "1000000")]
        public decimal MaxAmount { get; set; } = 10000.00m;

        [Range(1,1440)]
        public int PendingTimeoutMinutes { get; set; } = 30;

    }
}
