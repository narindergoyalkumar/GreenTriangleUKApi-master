using ApiSkeletonPoc.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ApiSkeletonPoc.Core.Insfrastucture;
using ApiSkeletonPoc.Core.Repositories;

namespace ApiSkeletonPoc.Core.Services
{
    public class BaseService<T> : IBaseService<T> where T:class
    {
        private readonly IBaseRepository<T> _repository;

        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Generic method to get all records
        /// </summary>
        /// <returns>list of class type</returns>
        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        /// <summary>
        ///Generic method to get all records with relational tables
        /// </summary>
        /// <param name="include"></param>
        /// <returns>list of class type</returns>
        public IEnumerable<T> GetAll(params string[] include)
        {
            return _repository.GetAll(include);
        }

        /// <summary>
        /// Generic method to get an single record
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a class</returns>
        public T GetById(int id)
        {
            return _repository.GetById(id);
        }

        /// <summary>
        /// Generic method to evaluate a predicate
        /// </summary>
        /// <param name="exp"></param>
        /// <returns>predicate</returns>
        public IEnumerable<T> Where(Expression<Func<T, bool>> exp)
        {
            return _repository.Where(exp);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> exp, string[] include)
        {
            return _repository.Where(exp, include).AsQueryable();
        }

        /// <summary>
        /// Generic method to add or update the record
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="Id"></param>
        /// <returns>a class</returns>
        public T AddOrUpdate(T entry, int Id)
        {
            var targetRecord = _repository.GetById(Id);
            var exists = targetRecord != null;

            if (exists)
            {
                _repository.Update(entry);
                return entry;
            }
            _repository.Insert(entry);
            return entry;
        }

        /// <summary>
        /// Generic methos to add or update with properties 
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="guid"></param>
        /// <param name="excludedProps"></param>
        /// <param name="excludedNavigationProperties"></param>
        /// <returns>class</returns>
        public T AddOrUpdate(T entry, Guid guid, string[] excludedProps = null, string[] excludedNavigationProperties = null)
        {
            var targetRecord = _repository.GetById(guid);
            var exists = targetRecord != null;
            if (exists)
            {
                _repository.Update(entry, excludedProps, excludedNavigationProperties);
                return entry;
            }
            _repository.Insert(entry);
            return entry;
        }

        /// <summary>
        /// Generic methos to add or update with properties 
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="Id"></param>
        /// <param name="excludedProps"></param>
        /// <param name="excludedNavigationProperties"></param>
        /// <returns>class</returns>
        public T AddOrUpdate(T entry, int id, string[] excludedProps = null, string[] excludedNavigationProperties = null)
        {
            var targetRecord = _repository.GetById(id);
            var exists = targetRecord != null;
            if (exists)
            {
                _repository.Update(entry, excludedProps, excludedNavigationProperties);
                return entry;
            }
            _repository.Insert(entry);
            return entry;
        }
        /// <summary>
        /// Generic method to remove a record
        /// </summary>
        /// <param name="id"></param>
        public void Remove(int id)
        {
            var label = _repository.GetById(id);
            _repository.Delete(label);
        }

        /// <summary>
        /// Generic method to insert bulk records
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public bool InsertBulk(IEnumerable<T> entities)
        {
            return _repository.InsertBulk(entities);
        }

        /// <summary>
        /// Generic method to get a single record
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public T GetById(Guid guid)
        {
            return _repository.GetById(guid);
        }

        public bool DeleteBulk(IEnumerable<T> entities)
        {
            return _repository.DeleteBulk(entities);
        }

        public IEnumerable<T> Get(out int count, Expression<Func<T, bool>> filter = null, string[] includePaths = null, int? page = null, int? pageSize = null, params SortExpression<T>[] sortExpressions)
        {
            return _repository.Get(out count, filter, includePaths, page, pageSize, sortExpressions);
        }

        public void Remove(Guid id)
        {
            var label = _repository.GetById(id);
            _repository.Delete(label);
        }

        public void TruncateTable(string tableName)
        {
            _repository.TruncateTable(tableName);
        }
        public void RunRawSql(string query)
        {
            _repository.RunRawSql(query);
        }
    }
}
