using LinkDevelopmentWorkshop.Repo.CustomRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevelopmentWorkshop.Repo.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        IUserRepository User
        {
            get;
        }
        ICategoryRepository Category
        {
            get;
        }
        IProductRepository Product
        {
            get;
        } 
        IOrderRepository Order
        {
            get;
        }
        int Save();
    }

}
