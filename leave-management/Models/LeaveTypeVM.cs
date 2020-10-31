using System;
using System.ComponentModel.DataAnnotations;

namespace leave_management.Models
{
    public class LeaveTypeVM
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Leave type name")]
        public string Name { get; set; }

        [Required]
        [Range(1,25, ErrorMessage = "Please enter a valid number")]
        [Display(Name = "Default number of days")]
        public int DefaultDays { get; set; }

        [Display(Name = "Date created")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateCreated { get; set; }
    }
}
