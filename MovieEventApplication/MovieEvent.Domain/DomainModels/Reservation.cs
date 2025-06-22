using MovieEvent.Domain.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieEvent.Domain.DomainModels
{
    public class Reservation : BaseEntity
    {
        public Guid ScreeningId { get; set; }
        public Screening? Screening { get; set; }
        public string? UserId { get; set; }
        public MovieUser? User { get; set; }

        public DateTime ReservationCreatedDate { get; set; } = DateTime.Now;

        public int ReservedSeats { get; set; }
        public double TotalPrice { get; set; }

    }
}
