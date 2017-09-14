using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HabitApp.Responses
{
    public interface IResponse
    {
        bool DidError { get; set; }
        string ErrorMessage { get; set; }
        string Message { get; set; }
    }
}
