using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieEvent.Domain.DomainModels;
using MovieEvent.Repository;
using MovieEvent.Service.Interface;

namespace MovieEvent.Web.Controllers
{
    public class ScreeningsController : Controller
    {
        private readonly IScreeningService _screeningService;

        public ScreeningsController(IScreeningService screeningService)
        {
             _screeningService = screeningService;
        }

        // GET: Screenings
        public IActionResult Index()
        {
            
            return View(_screeningService.GetAll());
        }

        // GET: Screenings/Details/5
        public IActionResult Details(Guid id)
        {
            var screening = _screeningService.GetById(id);
            if (screening == null)
            {
                return NotFound();
            }

            return View(screening);
        }

        // GET: Screenings/Create
        public IActionResult Create()
        {
           
            return View();
        }

        // POST: Screenings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("MovieId,AvailableSeats,TicketPrice,Id")] Screening screening)
        {
            if (ModelState.IsValid)
            {
                _screeningService.Insert(screening);
                return RedirectToAction(nameof(Index));
            }
            return View(screening);
        }

        // GET: Screenings/Edit/5
        public IActionResult Edit(Guid id)
        {
            var screening = _screeningService.GetById(id);
            if (screening == null)
            {
                return NotFound();
            }
            return View(screening);
        }

        // POST: Screenings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("MovieId,AvailableSeats,TicketPrice,Id")] Screening screening)
        {
            if (id != screening.Id)
            {
                return NotFound();
            }

            _screeningService.Update(screening);
            return RedirectToAction(nameof(Index));
        }

        // GET: Screenings/Delete/5
        public IActionResult Delete(Guid id)
        {
            var screening = _screeningService.GetById(id);
            if (screening == null)
            {
                return NotFound();
            }

            return View(screening);
        }

        // POST: Screenings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _screeningService.DeleteById(id);

            return RedirectToAction(nameof(Index));
        }

        //private bool ScreeningExists(Guid id)
        //{
        //    return _context.Screenings.Any(e => e.Id == id);
        //}
    }
}
