using LinkDevelopmentWorkshop.Domain.Entities;
using LinkDevelopmentWorkshop.Repo.GenericRepo;
using LinkDevelopmentWorkshop.Repo.Context;

namespace LinkDevelopmentWorkshop.Repo.CustomRepo
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDBContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
