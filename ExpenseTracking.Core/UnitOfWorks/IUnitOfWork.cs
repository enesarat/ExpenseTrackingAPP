using ExpenseTracking.Core.Models.Concrete;
using ExpenseTracking.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IGenericRepository<Role> RoleRepository { get; }
        Task CommitAsync();
        void Commit();
    }
}
