using SmartHouse.Api.Database;
using SmartHouse.Models.Models;

namespace SmartHouse.Api.Services
{
    public interface IHomeAddressService : IData<HomeAddress>
    {

    }
    public class HomeAddressService : Data<HomeAddress>, IHomeAddressService
    {
        public HomeAddressService(ApplicationDbContext context) : base(context)
        {

        }
    }
}
