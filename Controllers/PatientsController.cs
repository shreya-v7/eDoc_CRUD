using eDoc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDoc.Controllers
{
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Patient Patient { get; set; }
        public PatientsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Patient = new Patient();
            if (id == null)
            {
                return View(Patient);
            }

            Patient = _db.Patients.FirstOrDefault(u => u.Id == id);
            if (Patient == null)
            {
                return NotFound();
            }
            return View(Patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if (Patient.Id == 0)
                {
                    _db.Patients.Add(Patient);
                }
                else
                {
                    _db.Patients.Update(Patient);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Patient);
        }

        #region API Calls
        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Patients.ToListAsync() });
        }

        [HttpDelete]

        public async Task<IActionResult> Delete(int id)
        {
            var patientFromDb = await _db.Patients.FirstOrDefaultAsync(u => u.Id == id);
            if (patientFromDb == null)
            {
                return Json(new { success = false, message = "Error" });
            }

            _db.Patients.Remove(patientFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Deleted!" });
        }
        #endregion
    }
}
