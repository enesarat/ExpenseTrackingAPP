using ExpenseTracking.Core.Models.Concrete;
using ExpenseTracking.Core.Repositories;
using ExpenseTracking.Core.UnitOfWorks;
using ExpenseTracking.Repository.Contexts;
using ExpenseTracking.Repository.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Repository.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;
        public bool disposed;


        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public void Commit()
        {
            using (var dbContextTransction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    dbContext.SaveChanges();
                    dbContextTransction.Commit();
                }
                catch (Exception ex)
                {
                    // logging
                    dbContextTransction.Rollback();
                }
            }
        }

        public async Task CommitAsync()
        {
            using (var dbContextTransction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    await dbContext.SaveChangesAsync();
                    dbContextTransction.Commit();
                }
                catch (Exception ex)
                {
                    // logging
                    dbContextTransction.Rollback();
                }
            }
        }

        protected virtual void Clean(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Clean(true);
            GC.SuppressFinalize(this);
        }
    }
}
