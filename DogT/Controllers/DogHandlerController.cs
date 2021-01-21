using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogT.Data;
using DogT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DogT.Controllers
{
    [Authorize(Roles = "Кінолог")]
    public class DogHandlerController : Controller
    {
        private readonly DogTContext _context;

        public DogHandlerController(DogTContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dogs = await _context.Dogs
                .Include(d => d.DogHandler)
                .ThenInclude(u => u.User)
                .Include(s => s.Specialization)
                .Where(dh => dh.DogHandler.User.Email == User.Identity.Name)
                .AsNoTracking()
                .ToListAsync();

            return View(dogs);
        }

        public async Task<IActionResult> Dogs()
        {
            var dogs = await _context.Dogs
                .Include(d => d.DogHandler)
                .ThenInclude(u => u.User)
                .Include(s => s.Specialization)
                .Where(dh => dh.DogHandler.User.Email == User.Identity.Name)
                .AsNoTracking()
                .ToListAsync();

            return View(dogs);
        }

        [HttpGet]
        public IActionResult AddDog()
        {
            ViewBag.Specializations = new SelectList(_context.Specializations.ToList(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDog(Dog dog)
        {
            if (ModelState.IsValid)
            {
                dog.DogHandler = _context.DogHandlers.FirstOrDefault(d => d.User.Email == User.Identity.Name); ;
                _context.Dogs.Add(dog);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(dog);
        }

        public async Task<IActionResult> DetailsDog(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dog = await _context.Dogs
                .Include(dh => dh.DogHandler)
                .ThenInclude(u => u.User)
                .Include(s => s.Specialization)
                .FirstOrDefaultAsync(d => d.DogHandler.User.Email == User.Identity.Name && d.Id == id);

            if (dog == null)
            {
                return NotFound();
            }

            return View(dog);
        }

        [HttpGet]
        public async Task<IActionResult> EditDog(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dog = await _context.Dogs
                .Include(dh => dh.DogHandler)
                .ThenInclude(u => u.User)
                .Include(s => s.Specialization)
                .FirstOrDefaultAsync(d => d.DogHandler.User.Email == User.Identity.Name && d.Id == id);

            if (dog == null)
            {
                return NotFound();
            }

            ViewBag.Specializations = new SelectList(_context.Specializations.ToList(), "Id", "Title");

            return View(dog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDog(int id, [Bind("Id", "Name", "Age", "DogHandlerId", "SpecializationId")] Dog dog)
        {
            if (id != dog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(dog);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(dog);
        }

        [HttpGet]
        public async Task<IActionResult> ExcludeDog(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dog = await _context.Dogs
                .Include(dh => dh.DogHandler)
                .ThenInclude(u => u.User)
                .Include(s => s.Specialization)
                .FirstOrDefaultAsync(d => d.Id == id && d.DogHandler.User.Email == User.Identity.Name);

            if (dog == null)
            {
                return NotFound();
            }

            return View(dog);
        }

        [HttpPost, ActionName("ExcludeDog")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcludeConfirmed(int id)
        {
            var dog = await _context.Dogs
                .FirstOrDefaultAsync(d => d.Id == id && d.DogHandler.User.Email == User.Identity.Name);

            if (dog == null)
            {
                return Content("Something wrong!");
            }

            _context.Dogs.Remove(dog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Trainings()
        {
            var trainings = await _context.Trainings
                .Include(dh => dh.DogHandler)
                .ThenInclude(u => u.User)
                .Include(d => d.Dog)
                .Include(s => s.Specialization)
                .Where(t => t.DogHandler.User.Email == User.Identity.Name)
                .ToListAsync();

            return View(trainings);
        }

        [HttpGet]
        public IActionResult AddTraining()
        {
            ViewBag.Dogs = new SelectList(_context.Dogs
                .Where(d => d.DogHandler.User.Email == User.Identity.Name)
                .ToList(), "Id", "Name");

            ViewBag.Specializations = new SelectList(_context.Specializations.ToList(), "Id", "Title");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTraining(Training training)
        {
            ViewBag.Dogs = new SelectList(_context.Dogs
                .Where(d => d.DogHandler.User.Email == User.Identity.Name)
                .ToList(), "Id", "Name");

            ViewBag.Specializations = new SelectList(_context.Specializations.ToList(), "Id", "Title");

            if (ModelState.IsValid)
            {
                training.DogHandler = _context.DogHandlers.FirstOrDefault(d => d.User.Email == User.Identity.Name);
                _context.Trainings.Add(training);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Trainings));
            }

            return View(training);
        }

        public async Task<IActionResult> DetailsTraining(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var training = await _context.Trainings
                .Include(dh => dh.DogHandler)
                .ThenInclude(u => u.User)
                .Include(d => d.Dog)
                .Include(s => s.Specialization)
                .FirstOrDefaultAsync(t => t.DogHandler.User.Email == User.Identity.Name && t.Id == id);

            if (training == null)
            {
                return NotFound();
            }

            return View(training);
        }

        [HttpGet]
        public async Task<IActionResult> EditTraining(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var training = await _context.Trainings
                .Include(dh => dh.DogHandler)
                .ThenInclude(u => u.User)
                .Include(d => d.Dog)
                .Include(s => s.Specialization)
                .FirstOrDefaultAsync(t => t.DogHandler.User.Email == User.Identity.Name && t.Id == id);

            if (training == null)
            {
                return NotFound();
            }

            ViewBag.Dogs = new SelectList(_context.Dogs
                .Where(d => d.DogHandler.User.Email == User.Identity.Name)
                .ToList(), "Id", "Name");

            ViewBag.Specializations = new SelectList(_context.Specializations.ToList(), "Id", "Title");

            return View(training);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTraining(int id, [Bind("Id", "DogId", "DogHandlerId", "SpecializationId", "Context", "Estimate", "Date")] Training training)
        {
            if (id != training.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(training);
                await _context.SaveChangesAsync();

                return RedirectToAction("Trainings");
            }
            return View(training);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTraining(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var training = await _context.Trainings
                .Include(dh => dh.DogHandler)
                .ThenInclude(u => u.User)
                .Include(d => d.Dog)
                .Include(s => s.Specialization)
                .FirstOrDefaultAsync(t => t.DogHandler.User.Email == User.Identity.Name && t.Id == id);

            if (training == null)
            {
                return NotFound();
            }

            return View(training);
        }

        [HttpPost, ActionName("DeleteTraining")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTrainingConfirm(int id)
        {
            var training = await _context.Trainings
                .Include(dh => dh.DogHandler)
                .ThenInclude(u => u.User)
                .Include(d => d.Dog)
                .Include(s => s.Specialization)
                .FirstOrDefaultAsync(t => t.DogHandler.User.Email == User.Identity.Name && t.Id == id);

            if (training == null)
            {
                return Content("Something wrong!");
            }

            _context.Trainings.Remove(training);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Trainings));
        }

        public async Task<IActionResult> TrainingTasks()
        {
            var tasks = await _context.TrainingTasks
                .Include(dh => dh.DogHandler)
                .ThenInclude(u => u.User)
                .Include(d => d.Dog)
                .Where(dh => dh.DogHandler.User.Email == User.Identity.Name)
                .ToListAsync();

            return View(tasks);
        }
    }
}
