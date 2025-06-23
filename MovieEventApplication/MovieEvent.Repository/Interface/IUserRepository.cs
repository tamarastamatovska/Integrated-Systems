using MovieEvent.Domain.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieEvent.Repository.Interface
{
    public interface IUserRepository
    {
        MovieUser GetUserById(string id);
    }
}
