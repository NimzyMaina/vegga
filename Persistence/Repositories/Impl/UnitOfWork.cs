using System.Threading.Tasks;
using vegga.Persistence.Repositories.Contracts;

namespace vegga.Persistence.Repositories.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VeggaDbContext context;

        public UnitOfWork(VeggaDbContext context)
        {
            this.context = context;
        }
        public async Task Complete()
        {
            await context.SaveChangesAsync();
        }
    }
}