using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieEvent.Domain.DomainModels
{
    public class Screening : BaseEntity
    {

        public Guid MovieId { get; set; }
        public Movie? Movie { get; set; }

        public int AvailableSeats { get; set; }
        public int TotalSeats { get; set; }
        public int ReservedSeats { get; set; }
        public double TicketPrice { get; set; }
        public string? CinemaHall { get; set; }  

        public DateOnly ScreeningDate { get; set; }


        public virtual ICollection<Reservation>? Reservations { get; set; }


    }
}
