using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace leave_management.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class LeaveAllocationsController : Controller
    {
        //dependency injections
        private readonly ILeaveTypeRepository _leaveTypeRepo;
        private readonly ILeaveAllocationRepository _leaveAllocationRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;

        public LeaveAllocationsController(ILeaveTypeRepository leaveTypeRepo, ILeaveAllocationRepository leaveAllocationRepo, IMapper mapper, UserManager<Employee> userManager)
        {
            _leaveTypeRepo = leaveTypeRepo;
            _leaveAllocationRepo = leaveAllocationRepo;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: LeaveAllocationsController1
        public ActionResult Index()
        {
            var model = new CreateLeaveAllocationVM
            {
                LeaveTypes = _mapper.Map<ICollection<LeaveType>, ICollection<LeaveTypeVM>>(_leaveTypeRepo.FindAll()),
                NumberUpdated = 0
            };

            return View(model);
        }

        public ActionResult Setleave(int id)
        {
            var leaveType = _leaveTypeRepo.FindById(id);
            var employees = _userManager.GetUsersInRoleAsync("Employee").Result;

            foreach (var employee in employees)
            {
                if (_leaveAllocationRepo.CheckAllocation(id, employee.Id))
                    continue;

                var leaveAllocation = new LeaveAllocationVM
                {
                    DateCreated = DateTime.Now,
                    EmployeeId = employee.Id,
                    LeaveTypeId = id,
                    NumberOfDays = leaveType.DefaultDays,
                    Period = DateTime.Now.Year
                };

                _leaveAllocationRepo.Create(_mapper.Map<LeaveAllocation>(leaveAllocation));
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult ListEmployees()
        {
            var model = _mapper.Map<ICollection<EmployeeVM>>(_userManager.GetUsersInRoleAsync("Employee").Result);
            return View(model);
        }

        // GET: LeaveAllocationsController1/Details/5
        public ActionResult Details(string id)
        {
            var model = new ViewAllocationsVM
            {
                Employee = _mapper.Map<EmployeeVM>(_userManager.FindByIdAsync(id).Result),
                LeaveAllocations = _mapper.Map<ICollection<LeaveAllocationVM>>(_leaveAllocationRepo.GetLeaveAllocationsByEmployee(id))
            };
            return View(model);
        }

        // GET: LeaveAllocationsController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveAllocationsController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveAllocationsController1/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _mapper.Map<EditLeaveAllocationVM>(_leaveAllocationRepo.FindById(id));
            return View(model);
        }

        // POST: LeaveAllocationsController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditLeaveAllocationVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                //var allocation = _mapper.Map<LeaveAllocation>(model); does not work???
                var allocation = _leaveAllocationRepo.FindById(model.Id);
                allocation.NumberOfDays = model.NumberOfDays;
                var isSuccess = _leaveAllocationRepo.Update(allocation);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something has gone wrong!");
                    return View(model);
                }

                return RedirectToAction(nameof(Details), new { id = model.EmployeeId});
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "Something has gone wrong! " + ex.Message);
                return View(model);
            }
        }

        // GET: LeaveAllocationsController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveAllocationsController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
