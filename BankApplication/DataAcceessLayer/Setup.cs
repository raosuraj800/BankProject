using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DBLayer.Models

{
    public partial class Setup:DbContext
    {
        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            using (var context = new BankDatabaseContext()) {
                foreach (var entry in context.ChangeTracker.Entries().Where(e => e.State == (EntityState)EntityState.Added || e.State == (EntityState)EntityState.Modified))

                {
                    if (entry.State == EntityState.Added)
                    {
                        if (entry.Property("CreatedDate").CurrentValue == null || entry.Property("CreatedDate").CurrentValue.ToString() == default(DateTime).ToString())
                            entry.Property("CreatedDate").CurrentValue = DateTime.Now;
                        //if (entry.Property("CreatedBy").CurrentValue == null)
                        //    entry.Property("CreatedBy").CurrentValue = "Suraj";
                    }
                    if (entry.State == EntityState.Modified)
                    {
                        if (entry.Property("UpdatedDate").CurrentValue == null)
                            entry.Property("UpdatedDate").CurrentValue = DateTime.Now;
                        //if (entry.Property("UpdatedBy").CurrentValue == null)
                        //    entry.Property("UpdatedBy").CurrentValue = "Suraj";
                    }
                }
                return await base.SaveChangesAsync(cancellationToken);
            }
            //await ApplyAuditInformationAsync();
           
        }
        public override int SaveChanges()
        {

            foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == (EntityState)EntityState.Added || e.State == (EntityState)EntityState.Modified))

            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Property("CreatedDate").CurrentValue == null || entry.Property("CreatedDate").CurrentValue.ToString() == default(DateTime).ToString())
                        entry.Property("CreatedDate").CurrentValue = DateTime.Now;
                    //if (entry.Property("CreatedBy").CurrentValue == null)
                    //    entry.Property("CreatedBy").CurrentValue = "Suraj";
                }
                if (entry.State == EntityState.Modified)
                {
                    if (entry.Property("UpdatedDate").CurrentValue == null)
                        entry.Property("UpdatedDate").CurrentValue = DateTime.Now;
                    //if (entry.Property("UpdatedBy").CurrentValue == null)
                    //    entry.Property("UpdatedBy").CurrentValue = "Suraj";
                }
            }
            return base.SaveChanges();
        }

    }
}
