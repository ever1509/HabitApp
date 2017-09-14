using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HabitApp.Models.DataLayer.Contracts
{
    public interface IEncryptionService
    {
        string CrearSalt();
        string EncryptPassword(string pass, string salt);
    }
}
