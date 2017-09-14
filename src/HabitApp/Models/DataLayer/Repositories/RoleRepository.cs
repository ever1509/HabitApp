using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabitApp.Models.DataLayer.Contracts;
using HabitApp.Models.EntityLayer;

namespace HabitApp.Models.DataLayer.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(HabitAppDbContext dbContext)
            : base(dbContext)
        {

        }

        public override Role Get(Role entity)
        {
            return DbSet.FirstOrDefault(r => r.RoleId == entity.RoleId);
        }
    }
}
