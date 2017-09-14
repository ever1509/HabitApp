using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HabitApp.Responses
{
    public interface IListResponse<TModel>:IResponse
    {
        IEnumerable<TModel> Model { get; set; }
    }
}
