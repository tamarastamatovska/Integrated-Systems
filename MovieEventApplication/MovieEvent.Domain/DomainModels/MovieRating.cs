using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieEvent.Domain.DomainModels
{
    public class MovieRating : BaseEntity
    {

        public Guid MovieId { get; set; }
        public Movie? Movie { get; set; }
        public double Rating { get; set; }


        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
