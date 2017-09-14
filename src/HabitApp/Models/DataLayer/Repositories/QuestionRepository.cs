using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabitApp.Models.DataLayer.Contracts;
using HabitApp.Models.EntityLayer;

namespace HabitApp.Models.DataLayer.Repositories
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        public QuestionRepository(HabitAppDbContext dbContext)
            : base(dbContext)
        {

        }

        public override Question Get(Question entity)
        {
            return DbSet.FirstOrDefault(q => q.QuestionId == entity.QuestionId);
        }
    }
}
