using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DBLayer.UnitOfWork
{
    
        public interface IUnitOfWork<out BankDatabaseContext>
        where BankDatabaseContext : DbContext, new()
        {
        BankDatabaseContext Context { get; }
            void CreateTransaction();
            void Commit();
            void Rollback();
            void Save();

        }
    
}
