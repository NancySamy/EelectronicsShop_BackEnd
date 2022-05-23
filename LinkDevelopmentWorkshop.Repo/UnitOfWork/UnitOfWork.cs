using LinkDevelopmentWorkshop.Repo.Context;
using LinkDevelopmentWorkshop.Repo.CustomRepo;
using System;

namespace LinkDevelopmentWorkshop.Repo.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDBContext context ;
        private bool _disposed;

        public UnitOfWork(ApplicationDBContext dbContext)
        {
            this.context = dbContext;
            this.User = new UserRepository(dbContext);
            this.Product = new ProductRepository(dbContext);
            this.Order = new OrderRepository(dbContext);
            this.Category = new CategoryRepository(dbContext);
        }
        public IUserRepository User { get; private set; }
        public ICategoryRepository Category { get; private set; }

        public IProductRepository Product { get; private set; }

        public IOrderRepository Order { get; private set; }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public int Save()
        {
            return context.SaveChanges();
        }
    }
}