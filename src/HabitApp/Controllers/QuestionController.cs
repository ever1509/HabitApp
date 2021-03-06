﻿using System;
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
        public async Task<IActionResult> Get()
        {
            var response = new ListResponse<QuestionViewModel>();
            try
            {
                response.Model = await Task.Run(() =>
                {
                    return Mapper.Map<IEnumerable<Question>, IEnumerable<QuestionViewModel>>
                        (UnitOfWork.QuestionRepository.GetAll().ToList());
                });

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
        [HttpGet("{id}", Name = "GetQuestion")]
        public async Task<IActionResult> Get(int id)
        {
            var response = new SingleResponse<QuestionViewModel>();
            try
            {
                var result = UnitOfWork.QuestionRepository.Get(new Question(id));
                if (result != null)
                {
                    response.Model = await Task.Run(() =>
                    {
                        return Mapper.Map<Question, QuestionViewModel>(result);
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

        // POST: api/Question
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]QuestionViewModel value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = new SingleResponse<QuestionViewModel>();
            try
            {
                var q = Mapper.Map<QuestionViewModel, Question>(value);
                response.Model = await Task.Run(() =>
                {
                    UnitOfWork.QuestionRepository.Add(q);
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

        // PUT: api/Question/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]QuestionViewModel value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = new SingleResponse<QuestionViewModel>();
            try
            {
                Question q = Mapper.Map<QuestionViewModel, Question>(value);
                Question questionDb = UnitOfWork.QuestionRepository.Get(q);
                if (questionDb == null)
                {
                    return NotFound();
                }
                else
                {
                    response.Model = await Task.Run(() =>
                    {
                        questionDb.QuestionDate = value.QuestionDate;
                        questionDb.QuestionDescription = value.QuestionDescription;
                        questionDb.HabitId = value.HabitId;
                        UnitOfWork.CommitChanges();
                        return Mapper.Map<Question, QuestionViewModel>(questionDb);
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
        [HttpDelete("{id}", Name = "RemoveQuestion")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = new SingleResponse<QuestionViewModel>();
            try
            {
                Question questionDb = UnitOfWork.QuestionRepository.Get(new Question(id));
                if (questionDb == null)
                {
                    return NotFound();
                }
                else
                {
                    response.Model = await Task.Run(() =>
                    {
                        UnitOfWork.QuestionRepository.Remove(questionDb);
                        UnitOfWork.CommitChanges();
                        return Mapper.Map<Question, QuestionViewModel>(questionDb);
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
