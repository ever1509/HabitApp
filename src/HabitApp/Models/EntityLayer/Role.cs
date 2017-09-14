using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HabitApp.Models.EntityLayer
{
    [Table("Roles")]
    public class Role
    {
        public Role()
        {

        }

        public Role(int roleid)
        {
            RoleId = roleid;
        }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
