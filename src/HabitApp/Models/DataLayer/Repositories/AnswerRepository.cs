using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabitApp.Models.DataLayer.Contracts;
using HabitApp.Models.EntityLayer;

namespace HabitApp.Models.DataLayer.Repositories
{
    public class AnswerRepository : Repository<Answer>, IAnswerRepository
    {
        public AnswerRepository(HabitAppDbContext dbContext)
            : base(dbContext)
        {

        }

        public override Answer Get(Answer entity)
        {
            return DbSet.FirstOrDefault(a => a.AnswerId == entity.AnswerId);
        }
    }
}
