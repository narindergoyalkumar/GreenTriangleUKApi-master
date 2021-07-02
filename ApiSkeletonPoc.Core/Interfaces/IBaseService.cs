using ApiSkeletonPoc.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ApiSkeletonPoc.Core.Interfaces
{
    public interface IBaseService<T> where T:class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(params string[] includes);
        T GetById(int id);
        T GetById(Guid guid);
        IEnumerable<T> Where(Expression<Func<T, bool>> exp);
        IQueryable<T> Where(Expression<Func<T, bool>> exp,params string[] include);
        T AddOrUpdate(T entry, int Id);
        T AddOrUpdate(T entry, Guid guid, string[] excludedProps = null, string[] excludedNavigationProperties = null);
        T AddOrUpdate(T entry, int id, string[] excludedProps = null, string[] excludedNavigationProperties = null);
        void Remove(int id);
        void Remove(Guid id);
        bool InsertBulk(IEnumerable<T> entity);
        bool DeleteBulk(IEnumerable<T> entities);
        IEnumerable<T> Get(out int count,
           Expression<Func<T, bool>> filter = null,
           string[] includePaths = null,
           int? page = null,
           int? pageSize = null,
           params SortExpression<T>[] sortExpressions);
        void TruncateTable(string tableName);
        void RunRawSql(string query);
    }
}
