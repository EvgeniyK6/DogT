using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogT.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using DogT.Models;

namespace DogT.Controllers
{
    [Authorize(Roles = "Адміністратор")]
    public class AdminController : Controller
    {
        private readonly DogTContext _context;

        public AdminController(DogTContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dogHandlers = await _context.DogHandlers
                .Include(d => d.Dogs)
                .ToListAsync();
            return View(dogHandlers);
        }

        public IActionResult Dogs()
        {
            var dogs = _context.Dogs
                .Include(s => s.Specialization)
                .Include(dh => dh.DogHandler)
                .ToList();

            return View(dogs);
        }

        public IActionResult AddDog()
        {
            ViewBag.Specializations = new SelectList(_context.Specializations.ToList(), "Id", "Title");
            ViewBag.DogHandlers = new SelectList(_context.DogHandlers.ToList(), "Id", "Surname");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDog(Dog dog)
        {
            if (ModelState.IsValid)
            {
                _context.Dogs.Add(dog);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
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
                .Include(s => s.Specialization)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (dog == null)
            {
                return NotFound();
            }

            ViewBag.Specializations = new SelectList(_context.Specializations.ToList(), "Id", "Title");
            ViewBag.DogHandlers = new SelectList(_context.DogHandlers.ToList(), "Id", "Surname");

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

                return RedirectToAction("Dogs");
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
                .Include(s => s.Specialization)
                .FirstOrDefaultAsync(d => d.Id == id);

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
                .FirstOrDefaultAsync(d => d.Id == id);

            if (dog == null)
            {
                return Content("Something wrong!");
            }

            _context.Dogs.Remove(dog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Dogs));
        }

        public async Task<IActionResult> DetailsDog(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dog = await _context.Dogs
                .Include(dh => dh.DogHandler)
                .Include(s => s.Specialization)
                .Include(t => t.Trainings)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (dog == null)
            {
                return NotFound();
            }

            return View(dog);
        }

        public async Task<IActionResult> Tasks()
        {
            var tasks = await _context.TrainingTasks
                .Include(dh => dh.DogHandler)
                .Include(d => d.Dog)
                .ToListAsync();

            return View(tasks);
        }

        [HttpGet]
        public IActionResult AddTask()
        {
            int selectedIndex = 1;

            SelectList dogHandlers = new SelectList(_context.DogHandlers, "Id", "Surname", selectedIndex);
            ViewBag.DogHandlers = dogHandlers;

            SelectList dogs = new SelectList(_context.Dogs.Where(d => d.DogHandlerId == selectedIndex), "Id", "Name");
            ViewBag.Dogs = dogs;

            return View();
        }

        public IActionResult GetDogsByHandlers(int id)
        {
            return PartialView(_context.Dogs.Where(d => d.DogHandlerId == id).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTask(TrainingTask task)
        {
            if (ModelState.IsValid)
            {
                _context.TrainingTasks.Add(task);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Tasks));
            }

            return View(task);
        }
    }
}
