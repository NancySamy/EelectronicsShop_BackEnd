using LinkDevelopmentWorkshop.Domain.Entities;
using LinkDevelopmentWorkshop.Repo.GenericRepo;
using LinkDevelopmentWorkshop.Repo.Context;

namespace LinkDevelopmentWorkshop.Repo.CustomRepo
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDBContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
