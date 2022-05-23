using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevelopmentWorkshop.Repo.GenericRepo
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DbContext RepositoryContext { get; set; }

        public RepositoryBase(DbContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }

        public IQueryable<T> FindAll()
        {
            return this.RepositoryContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindAll(Expression<Func<T, object>> include)
        {
            return this.RepositoryContext.Set<T>().Include(include).AsNoTracking();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.RepositoryContext.Set<T>()
                .Where(expression).AsNoTracking();
        }
        public T FindFirstOrDefualtByCondition(Expression<Func<T, bool>> expression)
        {
            return this.RepositoryContext.Set<T>()
                .FirstOrDefault(expression);
        }
        public void Create(T entity)
        {
            this.RepositoryContext.Set<T>().Add(entity);
        }
        public void CreateRange(IEnumerable<T> entities)
        {
            this.RepositoryContext.Set<T>().AddRange(entities);
        }
        public void Update(T entity)
        {
            this.RepositoryContext.Set<T>().Update(entity);
        }
        public void UpdateRange(IEnumerable<T> entities)
        {
            this.RepositoryContext.Set<T>().UpdateRange(entities);
        }
        public void Delete(T entity)
        {
            this.RepositoryContext.Set<T>().Remove(entity);
        }
        public void DeleteBy(Expression<Func<T, bool>> expression)
        {
            this.RepositoryContext.Set<T>().RemoveRange(FindByCondition(expression));
        } 
        public void DeleteBy(Expression<Func<T, bool>> expression, Expression<Func<T, object>> include)
        {
            var list = this.RepositoryContext.Set<T>().Include(include).Where(expression);
            this.RepositoryContext.Set<T>().RemoveRange(list);
        }
        
    }
}
