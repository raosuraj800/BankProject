using System;
using System.Collections.Generic;

using System.Text;
using DBLayer.Models;
using DBLayer.UnitOfWork;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;



namespace DBLayer.Repository
{
    public class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
    {
        private DbSet<T> _entities;
        private string _errorMessage = string.Empty;
        private bool _isDisposed;
    
        public GenericRepository(IUnitOfWork<BankDatabaseContext> unitOfWork)
            : this(unitOfWork.Context)
        {
        }
        public GenericRepository(BankDatabaseContext context)
        {
            _isDisposed = false;
            Context = context;
        }
      
        public BankDatabaseContext Context { get; set; }
        public virtual IQueryable<T> Table
        {
            get { return Entities; }
        }
        protected virtual DbSet<T> Entities
        {
            //get { return _entities ??= (DBSet<T>?)Context.Set<T>(); }
            get { return _entities ?? (_entities = Context.Set<T>()); }
        }
        public void Dispose()
        {
            if (Context != null)
                Context.Dispose();
            _isDisposed = true;
        }
        public virtual IEnumerable<T> GetAll()
        {
            return Entities.ToList();
        }
        public virtual T GetById(object id)
        {
            return Entities.Find(id);
        }
        public virtual void Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");
                
                Entities.Add(entity);
                if (Context == null || _isDisposed)
                    Context = new BankDatabaseContext();
                //Context.SaveChanges(); commented out call to SaveChanges as Context save changes will be 
                //called with Unit of work
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        _errorMessage += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                throw new Exception(_errorMessage, dbEx);
            }
        }
        //public void BulkInsert(IEnumerable<T> entities)
        //{
        //    try
        //    {
        //        if (entities == null)
        //        {
        //            throw new ArgumentNullException("entities");
        //        }
        //        Context.Configuration.AutoDetectChangesEnabled = false;
        //        Context.Set<T>().AddRange(entities);
        //        Context.SaveChanges();
        //    }
        //    catch (DbEntityValidationException dbEx)
        //    {
        //        foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                _errorMessage += string.Format("Property: {0} Error: {1}", validationError.PropertyName,
        //                                     validationError.ErrorMessage) + Environment.NewLine;
        //            }
        //        }
        //        throw new Exception(_errorMessage, dbEx);
        //    }
        //}
        public virtual void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");
                if (Context == null || _isDisposed)
                    Context = new BankDatabaseContext();
                SetEntryModified(entity);
                //Context.SaveChanges(); commented out call to SaveChanges as Context save changes will be called with Unit of work
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        _errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                throw new Exception(_errorMessage, dbEx);
            }
        }
        public virtual void Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");
                if (Context == null || _isDisposed)
                    Context = new BankDatabaseContext();
                Entities.Remove(entity);
                //Context.SaveChanges(); commented out call to SaveChanges as Context save changes will be called with Unit of work
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        _errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                throw new Exception(_errorMessage, dbEx);
            }
        }
        public  virtual void SetEntryModified(T entity)
        {
           Context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
