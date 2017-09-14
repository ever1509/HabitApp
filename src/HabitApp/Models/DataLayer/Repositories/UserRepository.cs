using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabitApp.Models.DataLayer.Contracts;
using HabitApp.Models.EntityLayer;

namespace HabitApp.Models.DataLayer.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(HabitAppDbContext dbContext)
            : base(dbContext)
        {

        }

        public override User Get(User entity)
        {
            return DbSet.FirstOrDefault(u => u.UserId == entity.UserId);
        }
    }
}
