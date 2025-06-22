using Microsoft.AspNetCore.Identity;
using MovieEvent.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieEvent.Domain.IdentityModels
{
    public class MovieUser : IdentityUser
    {

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }
    }
}
