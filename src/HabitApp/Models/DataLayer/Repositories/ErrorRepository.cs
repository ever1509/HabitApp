using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabitApp.Models.DataLayer.Contracts;
using HabitApp.Models.EntityLayer;
using Microsoft.EntityFrameworkCore;

namespace HabitApp.Models.DataLayer.Repositories
{
    public class ErrorRepository : Repository<ErrorLog>, IErrorRepository
    {
        public ErrorRepository(HabitAppDbContext dbContext)
            : base(dbContext)
        {

        }

        public override ErrorLog Get(ErrorLog entity)
        {
            return DbSet.FirstOrDefault(e => e.ErrorId == entity.ErrorId);
        }

        public void AddErrorLog(Exception e)
        {
            ErrorLog el= new ErrorLog()
            {
                DateCreated = DateTime.Now,
                Message = e.Message,
                StackTrace = e.ToString()                
            };
            var dbEntityEntry = DbContext.Entry(el);

            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(el);
            }

        }
    }
}
