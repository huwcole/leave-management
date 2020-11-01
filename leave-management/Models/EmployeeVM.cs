using System;
using System.ComponentModel.DataAnnotations;

namespace leave_management.Models
{
    public class EmployeeVM
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "First name")]
        public string Firstname { get; set; }

        [Display(Name = "Last name")]
        public string Lastname { get; set; }

        [Display(Name = "Tax ID")]
        public string TaxId { get; set; }

        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Date joined")]
        public DateTime DateJoined { get; set; }
    }
}
