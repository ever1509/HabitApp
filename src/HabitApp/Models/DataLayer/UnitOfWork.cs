using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabitApp.Models.DataLayer.Contracts;
using HabitApp.Models.DataLayer.Repositories;

namespace HabitApp.Models.DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        protected HabitAppDbContext DbContext;
        protected bool Disposed = false;
        protected IAnswerRepository _answerRepository;
        protected IQuestionRepository _questionRepository;
        protected IHabitRepository _habitRepository;
        protected IUserRepository _userRepository;
        protected IRoleRepository _roleRepository;
        protected IUserRoleRepository _userRoleRepository;

        private IEncryptionService _encriptionService;
        private IErrorRepository _errorRepository;

        public UnitOfWork(HabitAppDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public int CommitChanges()
        {
            if (DbContext.ChangeTracker.HasChanges())
            {
                return DbContext.SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        protected void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                }
            }
            Disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public IAnswerRepository AnswerRepository => _answerRepository ??
                                                     (_answerRepository = new AnswerRepository(DbContext));
        public IQuestionRepository QuestionRepository => _questionRepository ?? (_questionRepository =
                                                             new QuestionRepository(DbContext));

        public IHabitRepository HabitRepository => _habitRepository ??
                                                   (_habitRepository = new HabitRepository(DbContext));

        public IUserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository(DbContext));
        public IRoleRepository RoleRepository => _roleRepository ?? (_roleRepository = new RoleRepository(DbContext));

        public IUserRoleRepository UserRoleRepository => _userRoleRepository ??
                                                         (_userRoleRepository = new UserRoleRepository(DbContext));

        public IEncryptionService EncryptionService => _encriptionService ?? (_encriptionService = new EncryptionService());

        public IErrorRepository ErrorRepository => _errorRepository ?? (_errorRepository = new ErrorRepository(DbContext));

    }
}
