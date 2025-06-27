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
using MovieEvent.Repository.Interface;
using MovieEvent.Service.Implementation;
using MovieEvent.Service.Interface;

namespace MovieEvent.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IDataFetchService _fetchService;
        private readonly IUserRepository _userRepository;
        private readonly IMovieRatingService _movieRatingService;
        private readonly IUserMovieRatingService _userMovieRatingService;

        public MoviesController(IMovieService movieService, IDataFetchService fetchService,IUserRepository userRepository, IMovieRatingService movieRatingService, IUserMovieRatingService userMovieRatingService)
        {
            _movieService = movieService;
            _fetchService = fetchService;
            _userRepository = userRepository;
            _movieRatingService = movieRatingService;
            _userMovieRatingService = userMovieRatingService;
        }

        // GET: Movies
        public IActionResult Index()
        {
            return View(_movieService.GetAll());
        }

        // GET: Movies/Details/5
        public IActionResult Details(Guid id)
        {
            var movie = _movieService.GetById(id);
            if (movie == null)
            {
                return NotFound();
            }
            var comments = _userMovieRatingService.GetCommentsForMovie(id);
            var averageRating = _movieService.GetAverageRating(id);

            ViewBag.Comments = comments;
            ViewBag.AverageRating = averageRating;
            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Description,PosterUrl,ReleaseDate,Duration,Genre,RANK,Year,RANKCategory")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _movieService.Insert(movie);
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public IActionResult Edit(Guid id)
        {
            var movie = _movieService.GetById(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Title,Description,PosterUrl,ReleaseDate,Duration,Genre,RANK,Year,RANKCategory,Id")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            _movieService.Update(movie);
            return RedirectToAction(nameof(Index));
        }

        // GET: Movies/Delete/5
        public IActionResult Delete(Guid id)
        {
            var movie = _movieService.GetById(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _movieService.DeleteById(id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> FetchMovies()
        {
            await _fetchService.FetchMoviesFromApi();
            return RedirectToAction(nameof(Index));
        }

        // GET: Movies/Rate/5
        public IActionResult Rate(Guid id)
        {
            var movie = _movieService.GetById(id);
            if (movie == null) return NotFound();

            var ratingModel = new MovieRating
            {
                MovieId = movie.Id
            };

            return View(ratingModel);
        }

        // POST: Movies/Rate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Rate(MovieRating model)
        {
            if (!ModelState.IsValid) return View(model);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var rating = new MovieRating
            {
                Id = Guid.NewGuid(),
                MovieId = model.MovieId,
                Rating = model.Rating,
                Comment = model.Comment,
                CreatedAt = DateTime.Now
            };

            var userMovieRating = new UserMovieRating
            {
                Id = Guid.NewGuid(),
                MovieRatingId = rating.Id,
                MovieRating = rating,
                UserId = userId,
                User = _userRepository.GetUserById(userId)
            };

            _movieRatingService.Insert(rating);
           

            _userMovieRatingService.Insert(userMovieRating);
            var movieForUpdate = _movieService.GetById(rating.MovieId); 
            movieForUpdate.MovieRatings.Add(rating);
            _movieService.Update(movieForUpdate);


            return RedirectToAction("Details", new { id = model.MovieId });
        }
        public IActionResult ViewScreenings(Guid id)
        {
            var movie = _movieService.GetByIdWithScreenings(id);

            if (movie == null)
            {
                return NotFound();
            }

            var screenings = movie.Screenings?.ToList() ?? new List<Screening>();

            return View(screenings);
        }


    }
}
