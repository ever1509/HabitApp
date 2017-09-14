using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HabitApp.Models.EntityLayer
{
    [Table("Habits")]
    public class Habit
    {
        public Habit()
        {

        }

        public Habit(int habitid)
        {
            HabitId = habitid;
        }

        public int HabitId { get; set; }
        public string HabitDescription { get; set; }
        public DateTime? HabitDate { get; set; }

        public Collection<Question> Questions { get; set; }

    }
}
