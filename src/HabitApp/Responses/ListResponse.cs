using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HabitApp.Responses
{
    public class ListResponse<TModel>:IListResponse<TModel>
    {
        public bool DidError { get; set; }
        public string ErrorMessage { get; set; }
        public string Message { get; set; }
        public IEnumerable<TModel> Model { get; set; }
    }
}
