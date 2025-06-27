using Microsoft.EntityFrameworkCore;
using MovieEvent.Domain.DomainModels;
using MovieEvent.Repository.Interface;
using MovieEvent.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieEvent.Service.Implementation
{
    public class ReservationService : IReservationService
    {

        private readonly IRepository<Reservation> _reservationRepository;
        private readonly IScreeningService _screeningService;

        public ReservationService(IRepository<Reservation> reservationRepository, IScreeningService screeningService)
        {
            _reservationRepository = reservationRepository;
            _screeningService = screeningService;
        }
        public Reservation DeleteById(Guid id)
        {
            var reservation = GetById(id);
            if (reservation == null)
            {
                throw new Exception("Movie not found");
            }
            _reservationRepository.Delete(reservation);
            return reservation;
        }

        public List<Reservation> GetAll()
        {
            return _reservationRepository.GetAll(selector: x => x,include:
                x=> x.Include(y=> y.Screening).ThenInclude(z=> z.Movie).Include(s=>s.User)).ToList();
        }

        public Reservation? GetById(Guid id)
        {
            return _reservationRepository.Get(selector: x => x,include: x=> x.
                                            Include(y=> y.Screening).ThenInclude(z=>z.Movie).Include(s => s.User),
                                          predicate: x => x.Id.Equals(id));
        }

        public Reservation Insert(Reservation reservation)
        {
            reservation.Id = Guid.NewGuid();
            return _reservationRepository.Insert(reservation);
        }

        public Reservation Update(Reservation updatedReservation)
        {
            var existingReservation = _reservationRepository.Get(selector:x=>x,predicate: x=> x.Id==updatedReservation.Id);
            if (existingReservation == null) throw new Exception("Reservation not found");

            int seatDifference = updatedReservation.ReservedSeats - existingReservation.ReservedSeats;

            var screening = _screeningService.GetById(existingReservation.ScreeningId);
            if (screening == null) throw new Exception("Screening not found");

            if (screening.AvailableSeats - seatDifference < 0)
                throw new Exception("Not enough seats available");

           
            screening.AvailableSeats -= seatDifference;

            _screeningService.Update(screening);

           
            existingReservation.ReservedSeats = updatedReservation.ReservedSeats;
            existingReservation.TotalPrice = updatedReservation.TotalPrice;
            existingReservation.ReservationCreatedDate = updatedReservation.ReservationCreatedDate;

           return _reservationRepository.Update(existingReservation);
        }
        public Reservation CreateReservation(Guid screeningId, string userId, int reservedSeats)
        {
            var screening = _screeningService.GetById(screeningId);

            if (screening == null)
                throw new Exception("Screening not found");

            if (reservedSeats > screening.AvailableSeats)
                throw new Exception("Not enough available seats");

           
            screening.AvailableSeats -= reservedSeats;
            screening.ReservedSeats += reservedSeats;
            _screeningService.Update(screening);

            
            var reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                ScreeningId = screeningId,
                UserId = userId,
                ReservedSeats = reservedSeats,
                TotalPrice = reservedSeats * screening.TicketPrice,
                ReservationCreatedDate = DateTime.Now
            };

            return _reservationRepository.Insert(reservation);
        }
        
    }
}
