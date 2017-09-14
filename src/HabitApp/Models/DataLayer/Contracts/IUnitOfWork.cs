using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HabitApp.Models.DataLayer.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IAnswerRepository AnswerRepository { get; }
        IQuestionRepository QuestionRepository { get; }
        IHabitRepository HabitRepository { get; }
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }

        IEncryptionService EncryptionService { get; }
        IErrorRepository ErrorRepository { get; }

        int CommitChanges();
    }
}
