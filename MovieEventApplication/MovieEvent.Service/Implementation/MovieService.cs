using Microsoft.EntityFrameworkCore;
using MovieEvent.Domain.DomainModels;
using MovieEvent.Repository.Interface;
using MovieEvent.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieEvent.Service.Implementation
{
  public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _movieRepository;
        public MovieService(IRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public Movie DeleteById(Guid id)
        {
            var movie = GetById(id);
            if (movie == null)
            {
                throw new Exception("Movie not found");
            }
            _movieRepository.Delete(movie);
            return movie;
        }

        public List<Movie> GetAll()
        {
            return _movieRepository.GetAll(selector:x=> x).ToList();
        }

        public Movie? GetById(Guid id)
        {
            return _movieRepository.Get(selector: x => x,
                                          predicate: x => x.Id.Equals(id));
        }
        public Movie? GetByIdWithScreenings(Guid id)
        {
            return _movieRepository.Get(
                selector: x => x,
                predicate: x => x.Id.Equals(id),
                   include: x => x.Include(y => y.Screenings)
            );
        }

        public Movie Insert(Movie movie)
        {
            movie.Id = Guid.NewGuid();
            return _movieRepository.Insert(movie);
        }

        public Movie Update(Movie movie)
        {
            return _movieRepository.Update(movie);
        }
        public ICollection<Movie> InsertMany(ICollection<Movie> movies)
        {
            return _movieRepository.InsertMany(movies);
        }

        public double GetAverageRating(Guid movieId)
        {
            var movie = GetById(movieId);
            if(movie == null)
            {
                throw new Exception("Movie not found");
            }

            if (movie.MovieRatings == null || !movie.MovieRatings.Any())
            {
                return 0; 
            }

            return movie.MovieRatings.Average(r => r.Rating);

        }
        public List<Movie> SearchByTitle(string text)
        {
            return _movieRepository.GetAll(
                selector: x => x,
                predicate: x => x.Title.ToLower().Contains(text.ToLower())
            ).ToList();
        }


    }
}
