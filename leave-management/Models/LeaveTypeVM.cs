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

        [Display(Name = "Date created")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateCreated { get; set; }
    }
}
