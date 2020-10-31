using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace leave_management.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class LeaveTypesController : Controller
    {
        //dependency injections
        private readonly ILeaveTypeRepository _leaveTypeRepo;
        private readonly IMapper _mapper;

        public LeaveTypesController(ILeaveTypeRepository leaveTypeRepo, IMapper mapper)
        {
            _leaveTypeRepo = leaveTypeRepo;
            _mapper = mapper;
        }

        // GET: LeaveTypesController
        public ActionResult Index()
        {
            var model = _mapper.Map<ICollection<LeaveType>, ICollection<LeaveTypeVM>>(_leaveTypeRepo.FindAll());

            return View(model);
        }

        public ActionResult Details(int id)
        {
            if (!_leaveTypeRepo.ItExists(id))
            {
                return Redirect("/Home/Error");
            }

            var model = _mapper.Map<LeaveTypeVM>(_leaveTypeRepo.FindById(id));

            return View(model);
        }

        // GET: LeaveTypesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LeaveTypeVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var leaveType = _mapper.Map<LeaveType>(model);
                leaveType.DateCreated = DateTime.Now;

                var isSuccess = _leaveTypeRepo.Create(leaveType);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something has gone wrong!");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something has gone wrong!");
                return View(model);
            }
        }

        // GET: LeaveTypesController/Edit/5
        public ActionResult Edit(int id)
        {
            if (!_leaveTypeRepo.ItExists(id))
            {
                return Redirect("/Home/Error");
            }

            var model = _mapper.Map<LeaveTypeVM>(_leaveTypeRepo.FindById(id));

            return View(model);
        }

        // POST: LeaveTypesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LeaveTypeVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var leaveType = _mapper.Map<LeaveType>(model);
                var isSuccess = _leaveTypeRepo.Update(leaveType);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something has gone wrong!");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something has gone wrong!");
                return View(model);
            }
        }

        // GET: LeaveTypesController/Delete/5
        public ActionResult Delete(int id)
        {
            if (!_leaveTypeRepo.ItExists(id))
            {
                return Redirect("/Home/Error");
            }

            var model = _mapper.Map<LeaveTypeVM>(_leaveTypeRepo.FindById(id));

            return View(model);
        }

        // POST: LeaveTypesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, LeaveTypeVM model)
        {
            try
            {
                var isSuccess = _leaveTypeRepo.Delete(_leaveTypeRepo.FindById(id));

                if (!isSuccess)
                {
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }
    }
}
