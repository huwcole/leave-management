﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using leave_management.Models;

namespace leave_management.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveHistory> LeaveHistories { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public DbSet<LeaveTypeVM> LeaveTypeVM { get; set; }
        public DbSet<EmployeeVM> EmployeeVM { get; set; }
        public DbSet<LeaveAllocationVM> LeaveAllocationVM { get; set; }
        public DbSet<EditLeaveAllocationVM> EditLeaveAllocationVM { get; set; }
    }
}
