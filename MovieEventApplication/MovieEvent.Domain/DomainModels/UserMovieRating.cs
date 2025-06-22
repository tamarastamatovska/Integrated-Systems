using MovieEvent.Domain.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieEvent.Domain.DomainModels
{
    public class UserMovieRating : BaseEntity
    {

        public Guid MovieRatingId { get; set; }
        public MovieRating? MovieRating { get; set; }
        public string? UserId { get; set; }
        public MovieUser? User { get; set; }

    }
}
