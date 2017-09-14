using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabitApp.Models.EntityLayer;

namespace HabitApp.Models.DataLayer.Contracts
{
    public interface IHabitRepository : IRepository<Habit>
    {
    }
}
