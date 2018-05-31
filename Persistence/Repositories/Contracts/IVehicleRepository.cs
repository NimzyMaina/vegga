using System.Threading.Tasks;
using vegga.Models;

namespace vegga.Persistence.Repositories.Contracts
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicleAsync(int id, bool includeRelated = true);

        void Add(Vehicle vehicle);

        void Remove(Vehicle vehicle);

        Task<Model> GetModel(int id);
    }
}