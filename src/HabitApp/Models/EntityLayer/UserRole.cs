using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HabitApp.Models.EntityLayer
{
    [Table("UserRoles")]
    public class UserRole
    {
        public UserRole()
        {

        }

        public UserRole(int userroleid)
        {
            UserRoleId = userroleid;
        }
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
