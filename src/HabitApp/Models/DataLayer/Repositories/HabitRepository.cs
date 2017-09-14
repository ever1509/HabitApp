using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabitApp.Models.DataLayer.Contracts;
using HabitApp.Models.EntityLayer;

namespace HabitApp.Models.DataLayer.Repositories
{
    public class HabitRepository : Repository<Habit>, IHabitRepository
    {
        public HabitRepository(HabitAppDbContext dbContext)
            : base(dbContext)
        {

        }

        public override Habit Get(Habit entity)
        {
            return DbSet.FirstOrDefault(h => h.HabitId == entity.HabitId);
        }
    }
}
