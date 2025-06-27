using MovieEvent.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieEvent.Service.Interface
{
    public interface IMovieService
    {
        List<Movie> GetAll();
        Movie? GetById(Guid id);
        Movie Insert(Movie movie);
        Movie Update(Movie movie);
        Movie DeleteById(Guid id);
        ICollection<Movie> InsertMany(ICollection<Movie> movies);
        double GetAverageRating(Guid movieId);
        Movie? GetByIdWithScreenings(Guid id);
    }
}
