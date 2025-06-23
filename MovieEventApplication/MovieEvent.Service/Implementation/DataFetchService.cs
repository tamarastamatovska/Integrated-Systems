using MovieEvent.Domain.DomainModels;
using MovieEvent.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using static System.Net.WebRequestMethods;
using MovieEvent.Domain.DTO;


namespace MovieEvent.Service.Implementation
{
    public class DataFetchService : IDataFetchService
    {
        private readonly HttpClient _httpClient;
        private readonly IMovieService _movieService;


        public DataFetchService(IHttpClientFactory httpClientFactory, IMovieService movieService)
        {
            _httpClient = httpClientFactory.CreateClient();
            _movieService = movieService;

        }

        
        public async Task<List<Movie>> FetchMoviesFromApi()
        {
            var allMovies = new List<Movie>();
            for (char letter = 'A'; letter <= 'Z'; letter++)
            {
                try
                {
                    var url = $"https://imdb.iamidiotareyoutoo.com/search?q={letter.ToString().ToLower()}";
                    HttpResponseMessage response = await _httpClient.GetAsync(url);

                   
                    var result = await response.Content.ReadFromJsonAsync<ApiResponse>();

                    if (result?.description != null)
                    {
                        var moviesForLetter = result.description.Select(x => new Movie
                        {
                            Id = Guid.NewGuid(),
                            Title = x.TITLE,
                            PosterUrl = x.IMG_POSTER,
                            Actors = x.ACTORS,
                            RANK = x.RANK,
                            Year = x.YEAR,
                            RANKCategory = GetRankCategory(x.RANK)
                        }).ToList();

                        allMovies.AddRange(moviesForLetter);
                        _movieService.InsertMany(moviesForLetter);
                    }

                    await Task.Delay(500);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching movies for letter {letter}: {ex.Message}");
                }
            }
            return allMovies;
        }


        private RANKCategory GetRankCategory(int rank)
        {
            return rank switch
            {
                <= 50 => RANKCategory.VeryGood,
                <= 100 => RANKCategory.Good,
                _ => RANKCategory.Bad
            };
        }
    }

    public class ApiResponse
    {
        public bool ok { get; set; }
        public List<MovieDTO> description { get; set; }
        public int error_code { get; set; }
    }
}
