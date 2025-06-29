using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieEvent.Domain.DomainModels;
using MovieEvent.Repository;
using MovieEvent.Service.Implementation;
using MovieEvent.Service.Interface;

namespace MovieEvent.Web.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IScreeningService _screeningService;

        public ReservationsController(IReservationService reservationService, IScreeningService screeningService)
        {
            _reservationService = reservationService;
            _screeningService = screeningService;
        }

        // GET: Reservations
        public IActionResult Index()
        {

            return View(_reservationService.GetAll());
        }

        // GET: Reservations/Details/5
        public IActionResult Details(Guid id)
        {
            var reservation = _reservationService.GetById(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        
        public IActionResult Create(Guid id)
        {
            var screening = _screeningService.GetById(id);
            if (screening == null)
            {
                return NotFound();
            }

            ViewBag.Screening = screening;
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Guid id, int reservedSeats)
        {
            var screening = _screeningService.GetById(id);
            if (screening == null)
            {
                return NotFound();
            }

            if (reservedSeats > screening.AvailableSeats)
            {
                ModelState.AddModelError("", "Not enough available seats.");
                ViewBag.Screening = screening;
                return View();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                _reservationService.CreateReservation(id, userId, reservedSeats);
                return RedirectToAction("MyReservations");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.Screening = screening;
                return View();
            }
        }

        // GET: Reservations/Edit/5
        public IActionResult Edit(Guid id)
        {
            var reservation = _reservationService.GetById(id);
            if (reservation == null)
            {
                return NotFound();
            }
            var screening = _screeningService.GetById(reservation.ScreeningId);
            ViewBag.TicketPrice = screening?.TicketPrice ?? 0;
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("ScreeningId,UserId,ReservationCreatedDate,ReservedSeats,TotalPrice,Id")] Reservation reservation)
        {
            
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                var screening = _screeningService.GetById(reservation.ScreeningId);
                ViewBag.TicketPrice = screening?.TicketPrice ?? 0;
                return View(reservation);
            }

            

            _reservationService.Update(reservation);
            return RedirectToAction("MyReservations");
        }



        // GET: Reservations/Delete/5
        public IActionResult Delete(Guid id)
        {
            var reservation = _reservationService.GetById(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _reservationService.DeleteById(id);

            return RedirectToAction(nameof(Index));
        }


        public IActionResult MyReservations()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var reservations = _reservationService.GetAll()
                .Where(r => r.UserId == userId)
                .ToList();

            return View("MyReservations", reservations);
        }

        //private bool ReservationExists(Guid id)
        //{
        //    return _context.Reservations.Any(e => e.Id == id);
        //}
        // GET: Reservations/Cancel/5
        public IActionResult Cancel(Guid id)
        {
            var reservation = _reservationService.GetById(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Cancel/5
        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        public IActionResult CancelConfirmed(Guid id)
        {
            try
            {
                _reservationService.CancelReservation(id);
                return RedirectToAction("MyReservations");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                var reservation = _reservationService.GetById(id);
                return View(reservation);
            }
        }
    }
}