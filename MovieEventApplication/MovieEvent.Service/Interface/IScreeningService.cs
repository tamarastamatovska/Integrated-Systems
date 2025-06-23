using MovieEvent.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieEvent.Service.Interface
{
    public interface IScreeningService
    {
        List<Screening> GetAll();
        Screening? GetById(Guid id);
        Screening Insert(Screening screening);
        Screening Update(Screening screening);
        Screening DeleteById(Guid id);
    }
}
