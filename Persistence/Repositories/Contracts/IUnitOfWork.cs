using System.Threading.Tasks;

namespace vegga.Persistence.Repositories.Contracts
{
    public interface IUnitOfWork
    {
         Task Complete();
    }
}