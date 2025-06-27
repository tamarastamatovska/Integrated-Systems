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
    public class ScreeningService : IScreeningService
    {

        private readonly IRepository<Screening> _screeningRepository;
        public ScreeningService(IRepository<Screening> screeningRepository)
        {
            _screeningRepository = screeningRepository;
        }
        public Screening DeleteById(Guid id)
        {
            var screening = GetById(id);
            if (screening == null)
            {
                throw new Exception("Screening not found");
            }
            _screeningRepository.Delete(screening);
            return screening;
        }

        public List<Screening> GetAll()
        {
            return _screeningRepository.GetAll(
             selector: x => x,
            include: x => x.Include(y => y.Movie)).ToList();
        }

        public Screening? GetById(Guid id)
        {
            return _screeningRepository.Get(selector: x => x,include:x=>x.Include(y=>y.Movie),
                                          predicate: x => x.Id.Equals(id));
        }

        public Screening Insert(Screening screening)
        {
            screening.Id = Guid.NewGuid();
            return _screeningRepository.Insert(screening);
        }

        public Screening Update(Screening screening)
        {
            return _screeningRepository.Update(screening);
        }
    }
}
