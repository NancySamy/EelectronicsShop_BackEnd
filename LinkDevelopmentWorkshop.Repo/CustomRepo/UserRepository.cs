using LinkDevelopmentWorkshop.Repo.Context;
using LinkDevelopmentWorkshop.Domain.Entities;
using LinkDevelopmentWorkshop.Repo.GenericRepo;

namespace LinkDevelopmentWorkshop.Repo.CustomRepo
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ApplicationDBContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
