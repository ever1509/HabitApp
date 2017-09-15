using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using AutoMapper;
using HabitApp.Models.DataLayer;
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
    [Route("api/User")]
    [EnableCors("AllowAnyOrigin")]
    public class UserController : Controller
    {
        protected IUnitOfWork UnitOfWork;
        public UserController(IUnitOfWork uow)
        {
            UnitOfWork = uow;
        }
        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = new ListResponse<UserViewModel>();
            try
            {
                response.Model = await Task.Run(() =>
                {
                    return Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>
                        (UnitOfWork.UserRepository.GetAll().ToList());
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

        // GET: api/User/5
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> Get(int id)
        {
            var response = new SingleResponse<UserViewModel>();
            try
            {
                var result = UnitOfWork.UserRepository.Get(new User(id));
                if (result != null)
                {
                    response.Model = await Task.Run(() =>
                    {
                        return Mapper.Map<User, UserViewModel>(result);
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

        // POST: api/User
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Post([FromBody]RegisterViewModel value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = new SingleResponse<RegisterViewModel>();
            try
            {
                response.Model = await Task.Run(() =>
                {
                    var u= Mapper.Map<User, UserViewModel>(
                        CreateUser(value.UserName, value.Email, value.Password, new int[] { value.RoleId }));                    
                        return u!= null ? value: null;                    
                });
                response.Message = "User created successfully.";
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

        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel user)
        {
            var response = new SingleResponse<LoginViewModel>();
            try
            {
                MembershipContext userContext = ValidateUser(user.Username, user.Password);
                if (userContext.User != null && userContext.User.IsLocked == false)
                {
                    user.RoleId = userContext.User.UserRoles.Select(x => x.RoleId).FirstOrDefault();
                    response.Model = await Task.Run(() =>
                    {
                        return user;
                    });
                    response.Message = "OK";
                }
                else
                {
                    response.Model = null;
                    response.Message = "User or Password incorrect";
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

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]UserViewModel value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = new SingleResponse<UserViewModel>();
            try
            {
                User u = Mapper.Map<UserViewModel, User>(value);
                User userDb = UnitOfWork.UserRepository.Get(u);
                if (userDb == null)
                {
                    return NotFound();
                }
                else
                {
                    response.Model = await Task.Run(() =>
                    {
                        userDb.DateCreated = value.DateCreated;
                        userDb.Email = value.Email;
                        userDb.IsLocked = value.IsLocked;
                        userDb.UserName = value.UserName;
                        userDb.HashedPassword = UnitOfWork.EncryptionService.EncryptPassword(value.Password, userDb.Salt);
                        UnitOfWork.CommitChanges();
                        return Mapper.Map<User, UserViewModel>(userDb);
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
        [HttpDelete("{id}", Name = "RemoveUser")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = new SingleResponse<UserViewModel>();
            try
            {
                User userDb = UnitOfWork.UserRepository.Get(new User(id));
                if (userDb == null)
                {
                    return NotFound();
                }
                else
                {
                    response.Model = await Task.Run(() =>
                    {
                        UnitOfWork.UserRepository.Remove(userDb);
                        UnitOfWork.CommitChanges();
                        return Mapper.Map<User, UserViewModel>(userDb);
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

        #region Membership Part
        public MembershipContext ValidateUser(string username, string password)
        {
            var membershipCtx = new MembershipContext();
            var user = UnitOfWork.UserRepository.GetSingleByUsername(username);
            if (user != null && IsUserValid(user, password))
            {
                var userRoles = GetUserRoles(user.UserId);
                membershipCtx.User = user;
                var identity = new GenericIdentity(user.UserName);
                membershipCtx.Principal = new GenericPrincipal(identity, userRoles.Select(x => x.RoleName).ToArray());
            }
            return membershipCtx;
        }

        public User CreateUser(string username, string email, string password, int[] roles)
        {
            var currentuser = UnitOfWork.UserRepository.GetSingleByUsername(username);
            if (currentuser != null)
            {
                throw new ApplicationException("User already in use");
            }
            var passSalt = UnitOfWork.EncryptionService.CrearSalt();
            var user = new User()
            {
                UserName = username,
                Email = email,
                Salt = passSalt,
                IsLocked = false,
                HashedPassword = UnitOfWork.EncryptionService.EncryptPassword(password, passSalt),
                DateCreated = DateTime.Now
            };
            UnitOfWork.UserRepository.Add(user);
            UnitOfWork.CommitChanges();

            if (roles != null || roles.Length > 0)
            {
                foreach (var role in roles)
                {
                    AddUserRole(user, role);
                }
            }
            UnitOfWork.CommitChanges();
            return user;
        }
        public User GetUser(int userId)
        {
            return UnitOfWork.UserRepository.Get(new User(userId));
        }
        public List<Role> GetUserRoles(int userId)
        {
            List<Role> result = new List<Role>();
            var currentUser = UnitOfWork.UserRoleRepository.GetRolesByUser(userId);
            if (currentUser != null)
            {
                foreach (var userRole in currentUser)
                {
                    result.Add(userRole.Role);
                }
            }
            return result.Distinct().ToList();
        }

        #region Private Methods
        private void AddUserRole(User user, int roleId)
        {
            var role = UnitOfWork.RoleRepository.Get(new Role(roleId));
            if (role == null)
            {
                throw new ApplicationException("Role doesn't exist.");
            }
            var userRole = new UserRole()
            {
                RoleId = role.RoleId,
                UserId = user.UserId
            };
            UnitOfWork.UserRoleRepository.Add(userRole);

        }
        private bool IsPassValid(User user, string password)
        {
            return string.Equals(UnitOfWork.EncryptionService.EncryptPassword(password, user.Salt), user.HashedPassword);
        }
        private bool IsUserValid(User user, string password)
        {
            if (IsPassValid(user, password))
            {
                return !user.IsLocked;
            }
            return false;
        }

        #endregion

        #endregion
    }
}
