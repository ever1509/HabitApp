using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HabitApp.Models.EntityLayer
{
    [Table("Users")]
    public class User
    {
        public User()
        {

        }
        public User(int userid)
        {
            UserId = userid;
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public bool IsLocked { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual Collection<UserRole> UserRoles { get; set; }
    }
}
