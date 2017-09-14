using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HabitApp.Models.EntityLayer
{
    [Table("Errors")]
    public class ErrorLog
    {
        public ErrorLog()
        {

        }

        public ErrorLog(int errorid)
        {
            ErrorId = errorid;
        }
        public int ErrorId { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime DateCreated { get; set; }        
    }
}
