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
    public class MovieRatingsController : Controller
    {
        private readonly IMovieRatingService _movieRatingService;

        public MovieRatingsController(IMovieRatingService movieRatingService)
        {
            _movieRatingService = movieRatingService;
        }

        // GET: MovieRatings
        public IActionResult Index()
        {
            
            return View(_movieRatingService.GetAll());
        }

        // GET: MovieRatings/Details/5
        public IActionResult Details(Guid id)
        {
            var movieRating = _movieRatingService.GetById(id);
            if (movieRating == null)
            {
                return NotFound();
            }

            return View(movieRating);
        }

        // GET: MovieRatings/Create
        public IActionResult Create()
        {
             return View();
        }

        // POST: MovieRatings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("MovieId,Rating,Comment,CreatedAt")] MovieRating movieRating)
        {
            if (ModelState.IsValid)
            {
                _movieRatingService.Insert(movieRating);
                return RedirectToAction(nameof(Index));
            }
            return View(movieRating);
        }

        // GET: MovieRatings/Edit/5
        public IActionResult Edit(Guid id)
        {
            var movieRating = _movieRatingService.GetById(id);
            if (movieRating == null)
            {
                return NotFound();
            }
            return View(movieRating);
        }

        // POST: MovieRatings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("MovieId,Rating,Comment,CreatedAt,Id")] MovieRating movieRating)
        {
            if (id != movieRating.Id)
            {
                return NotFound();
            }

            _movieRatingService.Update(movieRating);
            return RedirectToAction(nameof(Index));
        }

        // GET: MovieRatings/Delete/5
        public IActionResult Delete(Guid id)
        {
            var movieRating = _movieRatingService.GetById(id);
            if (movieRating == null)
            {
                return NotFound();
            }

            return View(movieRating);
        }

        // POST: MovieRatings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _movieRatingService.DeleteById(id);

            return RedirectToAction(nameof(Index));
        }

        //private bool MovieRatingExists(Guid id)
        //{
        //    return _context.MovieRatings.Any(e => e.Id == id);
        //}
    }
}
