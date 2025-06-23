using Microsoft.EntityFrameworkCore;
using MovieEvent.Domain.IdentityModels;
using MovieEvent.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieEvent.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly DbSet<MovieUser> entites;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            this.entites = _context.Set<MovieUser>();
        }

        public MovieUser GetUserById(string id)
        {
            return entites.First(ent => ent.Id == id);
        }
    }
}
