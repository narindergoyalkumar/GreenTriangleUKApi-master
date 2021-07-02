using ApiSkeletonPoc.Core.Insfrastucture.ErrorHandler;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.DAL.DataContracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace ApiSkeletonPoc.Core.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        private readonly GreenTriangleDbContext _context;

        private readonly DbSet<T> _entities;

        private readonly IErrorHandler _errorHandler;

        public BaseRepository(GreenTriangleDbContext context, IErrorHandler errorHandler)
        {
            _context = context;
            _entities = context.Set<T>();
            _errorHandler = errorHandler;
        }
        public IEnumerable<T> GetAll()
        {
            return _entities.ToList();
        }

        public IEnumerable<T> GetAll(params string[] include)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (string item in include)
            {
                query = query.Include(item);
            }
            return query.ToList();
        }
        public T GetById(int id)
        {
            return _entities.Find(id);
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> exp)
        {
            return _entities.Where(exp);
        }
        public IQueryable<T> Where(Expression<Func<T, bool>> exp, string[] include)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (string item in include)
            {
                query = query.Include(item);
            }
            return query.Where(exp).AsQueryable();
        }
        public T Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException(string.Format(_errorHandler.GetMessage(ErrorMessagesEnum.EntityNull), "", "Input data is null"));
            _entities.AddAsync(entity);
            _context.SaveChanges();
            return entity;
        }
        public void Update(T entity, string[] excludedProps = null, string[] excludedNavigationProperties = null)
        {
            if (entity == null) throw new ArgumentNullException(string.Format(_errorHandler.GetMessage(ErrorMessagesEnum.EntityNull), "", "Input data is null"));

            // Below code added if any entity already exists into local, then to update
            // that entity must need to detached first.
            var local = _entities.Local.ToList();
            foreach (var data in local)
            {
                if (data.GetType() == entity.GetType())
                {
                    _context.Entry(data).State = EntityState.Detached;
                }
            }
            _context.Entry(entity).State = EntityState.Modified;
            if (excludedProps != null)
            {
                if (excludedProps.Any())
                {
                    foreach (var name in excludedProps)
                    {
                        _context.Entry(entity).Property(name).IsModified = false;
                    }
                }
            }
            if (excludedNavigationProperties != null)
            {
                if (excludedNavigationProperties.Any())
                {
                    foreach (var name in excludedNavigationProperties)
                    {
                        _context.Entry(entity).Collection(name).IsModified = false;
                    }
                }
            }
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException(string.Format(_errorHandler.GetMessage(ErrorMessagesEnum.EntityNull), "", "Input data is null"));

            _entities.Remove(entity);
            _context.SaveChanges();
        }
        public bool InsertBulk(IEnumerable<T> entities)
        {
            bool result = false;
            if (entities.Count() > 0)
            {
                _entities.AddRange(entities);
                _context.SaveChanges();
                result = true;
            }
            return result;
        }
        public T GetById(Guid guid)
        {
            return _entities.Find(guid);
        }

        public bool DeleteBulk(IEnumerable<T> entities)
        {
            bool result = false;
            if (entities.Count() > 0)
            {
                _context.RemoveRange();
                result = true;
            }
            return result;
        }

        public IEnumerable<T> Get(out int count,
           Expression<Func<T, bool>> filter = null,
           string[] includePaths = null,
           int? page = null,
           int? pageSize = null,
           params SortExpression<T>[] sortExpressions)
        {
            IQueryable<T> query = _entities;
            count = query.Count();
            if (includePaths != null)
            {
                for (var i = 0; i < includePaths.Count(); i++)
                {
                    query = query.Include(includePaths[i]);
                }
            }
            if (filter != null)
            {
                query = query.Where(filter);
                count = query.Count();
            }
            if (sortExpressions != null)
            {
                if (sortExpressions.Length > 0)
                {
                    IOrderedQueryable<T> orderedQuery = null;
                    for (var i = 0; i < sortExpressions.Count(); i++)
                    {
                        if (i == 0)
                        {
                            if (sortExpressions[i].SortDirection == ListSortDirection.Ascending)
                            {
                                orderedQuery = query.OrderBy(sortExpressions[i].SortBy);
                            }
                            else
                            {
                                orderedQuery = query.OrderByDescending(sortExpressions[i].SortBy);
                            }
                        }
                        else
                        {
                            if (sortExpressions[i].SortDirection == ListSortDirection.Ascending)
                            {
                                orderedQuery = orderedQuery.ThenBy(sortExpressions[i].SortBy);
                            }
                            else
                            {
                                orderedQuery = orderedQuery.ThenByDescending(sortExpressions[i].SortBy);
                            }
                        }
                    }
                    query = orderedQuery;
                }
                //if (page != null)
                //{
                //    query = orderedQuery.Skip(((int)page - 1) * (int)pageSize);
                //}
            }

            if (pageSize != null)
            {
                query = query.Skip(((int)page - 1) * (int)pageSize).Take((int)pageSize);
            }
            return query.ToList();
        }
        public void TruncateTable(string tableName)
        {
            _context.Database.ExecuteSqlRaw($"truncate table {tableName}");
        }
        public void RunRawSql(string query)
        {
            _context.Database.ExecuteSqlRaw(query);
        }
    }
    public class SortExpression<T> where T : class
    {
        public SortExpression(Expression<Func<T, object>> sortBy, ListSortDirection sortDirection)
        {
            SortBy = sortBy;
            SortDirection = sortDirection;
        }
        public Expression<Func<T, object>> SortBy { get; set; }
        public ListSortDirection SortDirection { get; set; }
    }
}
