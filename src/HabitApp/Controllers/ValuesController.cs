using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabitApp.Models.DataLayer.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HabitApp.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAnyOrigin")]
    public class ValuesController : Controller
    {
        protected IUnitOfWork UnitOfWork;
        public ValuesController(IUnitOfWork uow)
        {
            UnitOfWork = uow;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var model = UnitOfWork.RoleRepository.GetAll();
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
