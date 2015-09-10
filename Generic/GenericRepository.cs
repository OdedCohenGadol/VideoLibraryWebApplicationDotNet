using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Generic.Interfaces;

namespace Generic.Repository
{
    /// <summary>
    /// Generic repository
    /// </summary>
    /// <typeparam name="T">The entity</typeparam>
    /// <typeparam name="TKey">the table key type</typeparam>
    public class GenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : class
    {
        protected DbSet<T> _dbSet;
        //private DbContext _context;

        /// <summary>
        /// so we can call stored procedures
        /// shiran 23.10.14
        /// </summary>
        protected DbContext _context;

        protected bool _isAutoCommit;

        protected bool _isReadOnly;

        public DbSet<T> DBSet
        {
            get { return this._dbSet; }
        }

        public GenericRepository(DbContext context, bool isAutoCommit = true, bool isReadOnly = false)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _isAutoCommit = isAutoCommit;
            _isReadOnly = isReadOnly;
        }

        /// <summary>
        /// Insert to DB
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(T entity)
        {
            if (!_isReadOnly)
            {
                try
                {
                        this._dbSet.Add(entity);
                        //this._dbSet.Attach(entity);
                        //this._context.Entry<T>(entity).State = EntityState.Added;

                        if (_isAutoCommit)
                        {
                            Commit();
                        }

                        //return entity./
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                throw new Exception("Not allowed action: ReadOnly.");
            }
        }

        /// <summary>
        /// Insert to DB
        /// </summary>
        /// <param name="entity"></param>
        public void InsertGraph(T entity)
        {
            if (!_isReadOnly)
            {
                this._dbSet.Add(entity);
                this._context.Entry<T>(entity).State = EntityState.Added;

                if (_isAutoCommit)
                {
                    Commit();
                }

                //return entity./
            }
            else
            {
                throw new Exception("Not allowed action: ReadOnly.");
            }
        }

        /// <summary>
        /// Delete from DB
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            if (!_isReadOnly)
            {
                this._dbSet.Remove(entity);

                if (_isAutoCommit)
                {
                    Commit();
                }
            }
            else
            {
                throw new Exception("Not allowed action: ReadOnly.");
            }
        }

        /// <summary>
        /// Update entity in DB
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            if (!_isReadOnly)
            {
                this._dbSet.Attach(entity);
                this._context.Entry<T>(entity).State = EntityState.Modified;

                if (_isAutoCommit)
                {
                    Commit();
                }
            }
            else
            {
                throw new Exception("Not allowed action: ReadOnly.");
            }
        }

        /// <summary>
        /// Get all records for <typeparamref name="T"/>
        /// </summary>
        public IQueryable<T> All
        {
            get { return this._dbSet; }
        }

        /// <summary>
        /// Get All including properties
        /// </summary>
        /// <param name="includeProperies">Including properties</param>
        /// <returns></returns>
        public IQueryable<T> AllIncluding(params System.Linq.Expressions.Expression<Func<T, object>>[] includeProperies)
        {
            IQueryable<T> query = this._dbSet;
            foreach (var includeProperty in includeProperies)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns></returns>
        public int Commit()
        {
            if (!_isReadOnly)
            {
                try
                {
                    return _context.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
            else
            {
                throw new Exception("Not allowed action: ReadOnly.");
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetByKey(TKey key)
        {
            return _dbSet.Find(key);
        }

        /// <summary>
        /// Call store procedure 
        /// </summary>
        /// <typeparam name="TResult">Expeted result type</typeparam>
        /// <param name="storeProcedureName">Store procedure name</param>
        /// <param name="parameters">store procedure parameters</param>
        /// <returns></returns>
        public IEnumerable<TResult> Exec<TResult>(string storeProcedureName, params object[] parameters)
        {
            //IEnumerable<TResult> result = _context.Database.SqlQuery<TResult>(String.Format("EXEC {0}", storeProcedureName),parameters);
            var aa = _context.GetType().GetMethods().Where(m => m.Name == storeProcedureName).ToList();
            MethodInfo method = _context.GetType().GetMethods().FirstOrDefault(m => m.Name == storeProcedureName && m.GetParameters().Count() == parameters.Count());

            IEnumerable<TResult> result = (IEnumerable<TResult>)method.Invoke(_context, parameters);
            
            //return result;
            return result;
        }

        public void Exec(string storeProcedureName, params object[] parameters)
        {
            //IEnumerable<TResult> result = _context.Database.SqlQuery<TResult>(String.Format("EXEC {0}", storeProcedureName),parameters);
            var aa = _context.GetType().GetMethods().Where(m => m.Name == storeProcedureName).ToList();
            MethodInfo method = _context.GetType().GetMethods().FirstOrDefault(m => m.Name == storeProcedureName && m.GetParameters().Count() == parameters.Count());
            method.Invoke(_context, parameters);
        }

    }
}
