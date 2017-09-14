using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabitApp.Models.DataLayer.Contracts;
using HabitApp.Models.EntityLayer;
using Microsoft.EntityFrameworkCore;

namespace HabitApp.Models.DataLayer.Repositories
{
    public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(HabitAppDbContext dbContext)
            : base(dbContext)
        {

        }

        public override UserRole Get(UserRole entity)
        {
            return DbSet.FirstOrDefault(ur => ur.UserRoleId == entity.UserRoleId);
        }
        public IEnumerable<UserRole> GetRolesByUser(int userId)
        {
            return DbSet.Where(ur => ur.UserId == userId).Include(ur => ur.Role).ToList();
        }
    }
}
