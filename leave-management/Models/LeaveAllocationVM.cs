﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace leave_management.Models
{
    public class LeaveAllocationVM
    {
        public int Id { get; set; }

        [Display(Name = "Number of days")]
        public int NumberOfDays { get; set; }

        [Display(Name = "Date created")]
        public DateTime DateCreated { get; set; }

        public int Period { get; set; }

        public EmployeeVM Employee { get; set; }

        public string EmployeeId { get; set; }

        [Display(Name = "Leave type")]
        public LeaveTypeVM LeaveType { get; set; }

        public int LeaveTypeId { get; set; }
    }

    public class CreateLeaveAllocationVM
    {
        public int NumberUpdated { get; set; }

        public ICollection<LeaveTypeVM> LeaveTypes { get; set; }
    }

    public class EditLeaveAllocationVM
    {
        public int Id { get; set; }

        public EmployeeVM Employee { get; set; }

        public string EmployeeId { get; set; }

        [Display(Name = "Number of days")]
        public int NumberOfDays { get; set; }

        [Display(Name = "Leave type")]
        public LeaveTypeVM LeaveType { get; set; }
    }

    public class ViewAllocationsVM
    {
        public EmployeeVM Employee { get; set; }

        public string EmployeeId { get; set; }

        public ICollection<LeaveAllocationVM> LeaveAllocations { get; set; }
    }
}
