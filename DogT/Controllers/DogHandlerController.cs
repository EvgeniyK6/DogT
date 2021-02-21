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
using DogT.ViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace DogT.Controllers
{
    [Authorize(Roles = "Кінолог")]
    public class DogHandlerController : Controller
    {
        private readonly DogTContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public DogHandlerController(DogTContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var trainings = await _context.Trainings
                .Include(dh => dh.DogHandler)
                .ThenInclude(u => u.User)
                .Include(d => d.Dog)
                .Include(s => s.Specialization)
                .ToListAsync();

            return View(trainings);
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
        public async Task<IActionResult> AddDog(Dog dog, IFormFile formFile)
        {
            if (ModelState.IsValid)
            {
                dog.DogHandler = _context.DogHandlers.FirstOrDefault(d => d.User.Email == User.Identity.Name);

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
                .Include(t => t.Trainings)
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
        public async Task<IActionResult> EditDog(int id, [Bind("Id", "Name", "Age", "DogHandlerId", "SpecializationId", "Avatar", "AvatarPath")] Dog dog)
        {
            if (id != dog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(dog);
                await _context.SaveChangesAsync();

                return RedirectToAction("DetailsDog", new { id = dog.Id});
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
            return RedirectToAction(nameof(Dogs));
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
        public async Task<IActionResult> AddTraining(Training training, IFormFile formFile)
        {
            ViewBag.Dogs = new SelectList(_context.Dogs
                .Where(d => d.DogHandler.User.Email == User.Identity.Name)
                .ToList(), "Id", "Name");

            ViewBag.Specializations = new SelectList(_context.Specializations.ToList(), "Id", "Title");

            if (ModelState.IsValid)
            {
                training.DogHandler = _context.DogHandlers.FirstOrDefault(d => d.User.Email == User.Identity.Name);
                var dog = await _context.Dogs.FirstOrDefaultAsync(d => d.Id == training.DogId);
                training.Specialization = dog.Specialization;

                if (formFile != null)
                {
                    string path = "/Videos/" + formFile.FileName;

                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await formFile.CopyToAsync(fileStream);
                    }

                    training.FileName = formFile.FileName;
                    training.FilePath = path;
                }

                _context.Trainings.Add(training);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(DetailsDog), new { id = training.Dog.Id});
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaskCompleted(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var task = await _context.TrainingTasks.FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            task.IsCompleted = true;

            _context.Update(task);
            _context.SaveChanges();

            return RedirectToAction("TrainingTasks", "DogHandler");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaskDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.TrainingTasks
                .Include(dh => dh.DogHandler)
                .ThenInclude(u => u.User)
                .FirstOrDefaultAsync(t => t.Id == id && t.DogHandler.User.Email == User.Identity.Name);

            if (task == null)
            {
                return NotFound();
            }

            _context.TrainingTasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction("TrainingTasks", "DogHandler");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LiveComment(int id, [Bind("CommentContext")] TrainingComment comment)
        {
            if (ModelState.IsValid)
            {
                comment.TrainingId = id;
                comment.DogHandler = await _context.DogHandlers
                    .Include(s => s.User)
                    .FirstOrDefaultAsync(u => u.User.Email == User.Identity.Name);
                comment.Date = DateTime.Now;
                
                _context.TrainingComments.Add(comment);
                await _context.SaveChangesAsync();

                return RedirectToAction("DetailsTraining", "DogHandler", new { id = id });
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Proposals()
        {
            var proposals = await _context.Proposals
                .Include(dh => dh.DogHandler)
                .ToListAsync();

            return View(proposals);
        }
        
        public IActionResult MakeProposal()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeProposal(Proposal proposal)
        {
            if (ModelState.IsValid)
            {
                proposal.Date = DateTime.Now;
                proposal.DogHandler = _context.DogHandlers.FirstOrDefault(d => d.User.Email == User.Identity.Name);

                _context.Proposals.Add(proposal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Proposals));
            }

            return View(proposal);
        }
    }
}
