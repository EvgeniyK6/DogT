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
using DogT.ViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace DogT.Controllers
{
    [Authorize(Roles = "Адміністратор")]
    public class AdminController : Controller
    {
        private readonly DogTContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public AdminController(DogTContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
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
        public async Task<IActionResult> AddDog(Dog dog, IFormFile formFile)
        {
            if (ModelState.IsValid)
            {
                if (formFile != null)
                {
                    string path = "/Avatars/" + formFile.FileName;

                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await formFile.CopyToAsync(fileStream);
                    }

                    dog.Avatar = formFile.FileName;
                    dog.AvatarPath = path;
                }

                _context.Dogs.Add(dog);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Dogs));
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaskDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.TrainingTasks.FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            _context.TrainingTasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Tasks));
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
                task.Date = DateTime.Now;
                
                _context.TrainingTasks.Add(task);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Tasks));
            }

            return View(task);
        }

        public async Task<IActionResult> DogHandlers()
        {
            var dogHandlers = await _context.DogHandlers
                .Include(d => d.Dogs)
                .ToListAsync();

            return View(dogHandlers);
        }

        public async Task<IActionResult> Trainings()
        {
            var trainings = await _context.Trainings
                .Include(dh => dh.DogHandler)
                .ThenInclude(u => u.User)
                .Include(d => d.Dog)
                .Include(s => s.Specialization)
                .ToListAsync();

            return View(trainings);
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
                .Include(c => c.Comments)
                .ThenInclude(dh => dh.DogHandler)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (training == null)
            {
                return NotFound();
            }

            TrainingViewModel viewModel = new TrainingViewModel
            {
                Id = training.Id,
                Dog = training.Dog,
                DogHandler = training.DogHandler,
                Specialization = training.Specialization,
                Context = training.Context,
                Date = training.Date,
                Comments = training.Comments,
                Estimate = training.Estimate,
                FileName = training.FileName,
                FilePath = training.FilePath
            };

            return View(viewModel);
        }
    }
}
