using MovieEvent.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieEvent.Service.Interface
{
    public interface IDataFetchService
    {
        Task<List<Movie>> FetchMoviesFromApi();
    }
}
