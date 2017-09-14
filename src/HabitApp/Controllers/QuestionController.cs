using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HabitApp.Models.DataLayer.Contracts;
using HabitApp.Models.EntityLayer;
using HabitApp.Responses;
using HabitApp.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HabitApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Question")]
    [EnableCors("AllowAnyOrigin")]
    public class QuestionController : Controller
    {
        protected IUnitOfWork UnitOfWork;
        // GET: api/Question
        public QuestionController(IUnitOfWork uow)
        {
            UnitOfWork = uow;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var response = new ListResponse<QuestionViewModel>();
            try
            {
                var result = UnitOfWork.QuestionRepository.GetAll();
                response.Model = Mapper.Map<IEnumerable<Question>,IEnumerable<QuestionViewModel>>(result);

            }
            catch (Exception e)
            {
                response.DidError = true;
                response.ErrorMessage = e.Message;
                response.Message = e.StackTrace;
            }
           return new OkObjectResult(response);
        }

        // GET: api/Question/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Question
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Question/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
