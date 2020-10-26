using System;
using System.ComponentModel.DataAnnotations;

namespace leave_management.Models
{
    public class DetailsLeaveTypeVM
    {
        public int Id { get; set; }

        [Display(Name = "Leave type name")]
        public string Name { get; set; }

        [Display(Name = "Date created")]
        public DateTime DateCreated { get; set; }
    }

    public class CreateLeaveTypeVM
    {
        [Required]
        public string Name { get; set; }
    }
}
