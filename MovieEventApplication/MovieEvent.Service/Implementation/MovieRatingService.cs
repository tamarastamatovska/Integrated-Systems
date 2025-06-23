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
    public class MovieRatingService : IMovieRatingService
    {

        private readonly IRepository<MovieRating> _movieRatingRepository;
        public MovieRatingService(IRepository<MovieRating> movieRatingRepository)
        {
            _movieRatingRepository = movieRatingRepository;
        }
        public MovieRating DeleteById(Guid id)
        {
            var movieRating = GetById(id);
            if (movieRating == null)
            {
                throw new Exception("Movie Rating not found");
            }
            _movieRatingRepository.Delete(movieRating);
            return movieRating;
        }

        public List<MovieRating> GetAll()
        {
            return _movieRatingRepository.GetAll(selector: x => x).ToList();
        }

        public MovieRating? GetById(Guid id)
        {
            return _movieRatingRepository.Get(selector: x => x,
                                         predicate: x => x.Id.Equals(id));
        }

        public MovieRating Insert(MovieRating movieRating)
        {
            movieRating.Id = Guid.NewGuid();
            return _movieRatingRepository.Insert(movieRating);
        }

        public MovieRating Update(MovieRating movieRating)
        {
            return _movieRatingRepository.Update(movieRating);
        }
    }
}
