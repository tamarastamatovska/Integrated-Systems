using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieEvent.Domain.DomainModels
{
    public enum RANKCategory
    {
        Bad,
        Good,
        VeryGood
    }
    public class Movie : BaseEntity
    {

        public string? Title { get; set; }
        
        public string? PosterUrl { get; set; }
        
        
        public string? Actors { get; set; }
        public int RANK { get; set; }

        public int Year { get; set; }
        public RANKCategory RANKCategory { get; set; }
        public virtual ICollection<MovieRating>? MovieRatings { get; set; }
        public virtual ICollection<Screening>? Screenings { get; set; }



    }
}
