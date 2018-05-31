using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vegga.Models;
using vegga.Persistence.Repositories.Contracts;

namespace vegga.Persistence.Repositories.Impl
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VeggaDbContext context;

        public VehicleRepository(VeggaDbContext context)
        {
            this.context = context;
        }

        public async Task<Vehicle> GetVehicleAsync(int id, bool includeRelated = true)
        {
            if(!includeRelated)
                return await context.Vehicles.FindAsync(id);

            return await context.Vehicles
            .Include(v => v.Model)
                .ThenInclude(vm => vm.Make)
            .Include(v => v.Features)
                .ThenInclude(vf => vf.Feature)
            .SingleOrDefaultAsync(v => v.Id == id);
        }

        public void Add(Vehicle vehicle)
        {
          context.Add(vehicle); 
        }

        public void Remove(Vehicle vehicle)
        {
            context.Remove(vehicle);
        }

        public async Task<Model> GetModel(int id)
        {
            return await context.Models.FindAsync(id);
        }
    }
}