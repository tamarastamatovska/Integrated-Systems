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
    public class UserMovieRatingService : IUserMovieRatingService
    {
        private readonly IRepository<UserMovieRating> _userMovieRatingRepository;

        public UserMovieRatingService(IRepository<UserMovieRating> userMovieRatingRepository)
        {
            _userMovieRatingRepository = userMovieRatingRepository;
        }

        public UserMovieRating Insert(UserMovieRating umr)
        {
            umr.Id = Guid.NewGuid();
            return _userMovieRatingRepository.Insert(umr);
        }

        //public List<UserMovieRating> GetRatingsByUser(string userId)
        //{
        //    return _userMovieRatingRepository
        //        .GetAll(selector: x => x, predicate: x => x.UserId == userId)
        //        .ToList();
        //}

        //public List<UserMovieRating> GetRatingsForMovie(Guid movieId)
        //{
        //    return _userMovieRatingRepository
        //        .GetAll(selector: x => x, predicate: x => x.MovieRating.MovieId == movieId)
        //        .ToList();
        //}

        public List<UserMovieRating> GetAll()
        {
            return _userMovieRatingRepository.GetAll(selector: x => x).ToList();
        }

        public UserMovieRating? GetById(Guid id)
        {
            var userMovieRating = _userMovieRatingRepository.Get(selector: x=>x,predicate: x=>x.Id == id);
            if(userMovieRating == null)
            {
                throw new Exception("User Movie Rating not found");

            }   
            return userMovieRating;
        }

        public UserMovieRating Update(UserMovieRating userMovieRating)
        {
            return _userMovieRatingRepository.Update(userMovieRating);
        }

        public UserMovieRating DeleteById(Guid id)
        {
            var userMovieRating = GetById(id);
            if(userMovieRating == null)
            {
                throw new Exception("User Movie Rating not found");
            }
            return _userMovieRatingRepository.Delete(userMovieRating);
        }
       


        public List<UserMovieRating> GetCommentsForMovie(Guid movieId)
        {
                    return _userMovieRatingRepository
                    .GetAll(
                    selector: x => x,
                    include: x => x
                        .Include(y => y.User)
                        .Include(y => y.MovieRating),
                        predicate: x => x.MovieRating.MovieId == movieId
                )
                .ToList();
        }
    }
}
