using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HabitApp.Models.EntityLayer
{
    [Table("Questions")]
    public class Question
    {
        public Question()
        {

        }

        public Question(int questionid)
        {
            QuestionId = questionid;
        }
        public int QuestionId { get; set; }
        public string QuestionDescription { get; set; }
        public DateTime? QuestionDate { get; set; }
        public int? HabitId { get; set; }

        public Collection<Answer> Answers { get; set; }
        public virtual Habit FkHabit { get; set; }
    }
}
