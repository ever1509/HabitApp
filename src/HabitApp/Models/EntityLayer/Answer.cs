using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HabitApp.Models.EntityLayer
{
    [Table("Answers")]
    public class Answer
    {
        public Answer()
        {

        }

        public Answer(int answerid)
        {
            AnswerId = answerid;
        }

        public int AnswerId { get; set; }
        public int? QuestionId { get; set; }
        public string AnswerDescription { get; set; }

        public virtual Question FkQuestion { get; set; }
    }
}
