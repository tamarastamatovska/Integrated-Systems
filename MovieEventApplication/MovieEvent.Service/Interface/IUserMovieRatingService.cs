using MovieEvent.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieEvent.Service.Interface
{
    public interface IUserMovieRatingService
    {
        List<UserMovieRating> GetAll();
        UserMovieRating? GetById(Guid id);
        UserMovieRating Insert(UserMovieRating userMovieRating);
        UserMovieRating Update(UserMovieRating userMovieRating);
        UserMovieRating DeleteById(Guid id);
        //List<UserMovieRating> GetRatingsByUser(string userId);
        //List<UserMovieRating> GetRatingsForMovie(Guid movieId);
        public List<UserMovieRating> GetCommentsForMovie(Guid movieId);
     


    }
}
