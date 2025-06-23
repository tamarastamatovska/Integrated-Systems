using MovieEvent.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieEvent.Service.Interface
{
    public interface IMovieRatingService
    {
        List<MovieRating> GetAll();
        MovieRating? GetById(Guid id);
        MovieRating Insert(MovieRating movieRating);
        MovieRating Update(MovieRating movieRating);
        MovieRating DeleteById(Guid id);
    }
}
