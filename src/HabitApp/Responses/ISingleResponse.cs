using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HabitApp.Responses
{
    public interface ISingleResponse<TModel>:IResponse
    {
        TModel Model { get; set; }
    }
}
