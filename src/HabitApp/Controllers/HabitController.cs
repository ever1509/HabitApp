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
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HabitApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Habit")]
    [EnableCors("AllowAnyOrigin")]
    public class HabitController : Controller
    {
        protected IUnitOfWork UnitOfWork;
        public HabitController(IUnitOfWork uow)
        {
            UnitOfWork = uow;
        }
        // GET: api/Habit
        [HttpGet]
        public async Task<IActionResult> Get()
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
            return new OkObjectResult(response);
        }
        // POST: api/Habit
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]HabitViewModel value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = new SingleResponse<HabitViewModel>();
            try
            {
                var h = Mapper.Map<HabitViewModel, Habit>(value);
                response.Model = await Task.Run(() =>
                {
                    UnitOfWork.HabitRepository.Add(h);
                    UnitOfWork.CommitChanges();
                    return value;
                });
                response.Message = "Record Inserted successfully.";
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

        // PUT: api/Habit/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]HabitViewModel value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = new SingleResponse<HabitViewModel>();
            try
            {
                Habit h = Mapper.Map<HabitViewModel, Habit>(value);
                Habit habitDb = UnitOfWork.HabitRepository.Get(h);
                if (habitDb == null)
                {
                    return NotFound();
                }
                else
                {
                    response.Model = await Task.Run(() =>
                    {
                        habitDb.HabitDate = value.HabitDate;
                        habitDb.HabitDescription = value.HabitDescription;
                        UnitOfWork.CommitChanges();
                        return Mapper.Map<Habit, HabitViewModel>(habitDb);
                    });
                    response.Message = "Record updated successfully!";
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
            return new OkObjectResult(response);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}", Name = "RemoveHabit")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = new SingleResponse<HabitViewModel>();
            try
            {
                Habit habitDb = UnitOfWork.HabitRepository.Get(new Habit(id));
                if (habitDb == null)
                {
                    return NotFound();
                }
                else
                {
                    response.Model = await Task.Run(() =>
                    {
                        UnitOfWork.HabitRepository.Remove(habitDb);
                        UnitOfWork.CommitChanges();
                        return Mapper.Map<Habit, HabitViewModel>(habitDb);
                    });
                    response.Message = "Record deleted successfully!";
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
            return new OkObjectResult(response);
        }
    }
}
