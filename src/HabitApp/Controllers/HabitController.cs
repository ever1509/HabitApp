using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using HabitApp.Models.DataLayer.Contracts;
using HabitApp.Models.EntityLayer;
using HabitApp.Responses;
using HabitApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HabitApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Habit")]
    public class HabitController : Controller
    {
        protected IUnitOfWork UnitOfWork;
        public HabitController(IUnitOfWork uow)
        {
            UnitOfWork = uow;
        }
        // GET: api/Habit
        [HttpGet]
        public async Task<IActionResult>  Get()
        {
            var response = new ListResponse<HabitViewModel>();
            try
            {                                
                response.Model = await Task.Run(() =>
                {
                    return Mapper.Map<IEnumerable<Habit>, IEnumerable<HabitViewModel>>
                        (UnitOfWork.HabitRepository.GetAll().ToList());
                });
            }
            catch (Exception e)
            {
                response.DidError = true;
                response.ErrorMessage = e.Message;
                response.Message = e.StackTrace;
                UnitOfWork.ErrorRepository.AddErrorLog(e);
                UnitOfWork.CommitChanges();
            }
            return new OkObjectResult(response);
        }

        // GET: api/Habit/5
        [HttpGet("{id}", Name = "GetHabit")]
        public async Task<IActionResult> Get(int id)
        {
            var response = new SingleResponse<HabitViewModel>();
            try
            {
                var result = UnitOfWork.HabitRepository.Get(new Habit(id));
                if (result != null)
                {
                    response.Model = await Task.Run(() =>
                    {
                        return Mapper.Map<Habit, HabitViewModel>(result);
                    });
                }
                else
                {
                    return NotFound();
                }
            }                            
            catch (Exception e)
            {
                response.DidError = true;
                response.ErrorMessage = e.Message;
                response.Message = e.StackTrace;
                UnitOfWork.ErrorRepository.AddErrorLog(e);
                UnitOfWork.CommitChanges();
            }
            return  new OkObjectResult(response);
        }        
        // POST: api/Habit
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Habit/5
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
