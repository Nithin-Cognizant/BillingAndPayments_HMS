using BillingAndPayments.BusinessLogic.Interfaces;
using BillingAndPayments.Repository.Models;
using BillingAndPayments.Repository;
using DoctorManagementSystem.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using BillingAndPayments.BusinessLogic.Services; // Make sure this is included

namespace DoctorManagementSystem.UI.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        // GET: Doctor/Register
        [HttpGet]
        public IActionResult Register()
        {
            var model = new DoctorRegisterViewModel
            {
                Availabilities = new List<AvailabilityViewModel>
                {
                    new AvailabilityViewModel() // Start with one empty row
                }
            };
            return View(model);
        }
        // POST: Doctor/Register
        [HttpPost]
        public async Task<IActionResult> Register(DoctorRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            int experienceYears = 0;
            if (model.ExperienceYears.HasValue)
            {
                experienceYears = model.ExperienceYears.Value;
            }
            else
            {
                // Handle the case where ExperienceYears is null.
                // You might want to add a model error if it's a required field.
                experienceYears = 0; // Default to 0 if not provided
            }

            var doctor = new Doctor
            {
                FullName = model.FullName,
                Email = model.Email,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                Specialty = model.Speciality,
                Qualification = model.Qualification,
                ExperienceYears = experienceYears,
                Availability = model.Availabilities.Select(a => new Availability
                {
                    Day = a.Day,
                    TimeSlot = a.TimeSlot
                }).ToList()
            };

            var success = await _doctorService.RegisterDoctorAsync(doctor);

            if (!success)
            {
                ModelState.AddModelError("", "Email already registered.");
                return View(model);
            }

            TempData["Success"] = "Registration successful! Please log in.";
            return RedirectToAction("Login");
        }
        // GET: Doctor/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        // POST: Doctor/Login
        [HttpPost]
        public async Task<IActionResult> Login(DoctorLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var doctor = await _doctorService.AuthenticateDoctorAsync(model.Email, model.Password);
            if (doctor == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View(model);
            }
            TempData["DoctorName"] = doctor.FullName;
            HttpContext.Session.SetString("DoctorEmail", doctor.Email);
            HttpContext.Session.SetString("DoctorName", doctor.FullName);
            // For now, just redirect to a placeholder page (we'll build the dashboard later)
            return RedirectToAction("Dashboard", "Doctor");
        }
        // GET: Doctor/Dashboard
        public IActionResult Dashboard()
        {
            var doctorEmail = HttpContext.Session.GetString("DoctorEmail");
            if (string.IsNullOrEmpty(doctorEmail))
                return RedirectToAction("Login");
            ViewBag.DoctorName = HttpContext.Session.GetString("DoctorName");
            return View();
        }
        // LogOut action
        public IActionResult LogOut()
        {
            TempData.Clear();
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Doctor");
        }
    }
}
