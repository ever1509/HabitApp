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
    [Route("api/Answer")]
    [EnableCors("AllowAnyOrigin")]
    public class AnswerController : Controller
    {
        protected IUnitOfWork UnitOfWork;
        public AnswerController(IUnitOfWork uow)
        {
            UnitOfWork = uow;
        }
        // GET: api/Answer
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = new ListResponse<AnswerViewModel>();
            try
            {
                response.Model = await Task.Run(() =>
                {
                    return Mapper.Map<IEnumerable<Answer>, IEnumerable<AnswerViewModel>>
                        (UnitOfWork.AnswerRepository.GetAll().ToList());
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

        // GET: api/Answer/5
        [HttpGet("{id}", Name = "GetAnswer")]
        public async Task<IActionResult> Get(int id)
        {
            var response = new SingleResponse<AnswerViewModel>();
            try
            {
                var result = UnitOfWork.AnswerRepository.Get(new Answer(id));
                if (result != null)
                {
                    response.Model = await Task.Run(() =>
                    {
                        return Mapper.Map<Answer, AnswerViewModel>(result);
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

        // POST: api/Answer
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AnswerViewModel value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = new SingleResponse<AnswerViewModel>();
            try
            {
                var a = Mapper.Map<AnswerViewModel, Answer>(value);
                response.Model = await Task.Run(() =>
                {
                    UnitOfWork.AnswerRepository.Add(a);
                    UnitOfWork.CommitChanges();
                    return value;
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

        // PUT: api/Answer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]AnswerViewModel value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = new SingleResponse<AnswerViewModel>();
            try
            {
                Answer a = Mapper.Map<AnswerViewModel, Answer>(value);
                Answer answerDb = UnitOfWork.AnswerRepository.Get(a);
                if (answerDb == null)
                {
                    return NotFound();
                }
                else
                {
                    response.Model = await Task.Run(() =>
                    {
                        answerDb.AnswerDescription = value.AnswerDescription;
                        answerDb.QuestionId = value.QuestionId;
                        UnitOfWork.CommitChanges();
                        return Mapper.Map<Answer, AnswerViewModel>(answerDb);
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
        [HttpDelete("{id}", Name = "RemoveAnswer")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = new SingleResponse<AnswerViewModel>();
            try
            {
                Answer answerDb = UnitOfWork.AnswerRepository.Get(new Answer(id));
                if (answerDb == null)
                {
                    return NotFound();
                }
                else
                {
                    response.Model = await Task.Run(() =>
                    {
                        UnitOfWork.AnswerRepository.Remove(answerDb);
                        UnitOfWork.CommitChanges();
                        return Mapper.Map<Answer, AnswerViewModel>(answerDb);
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
