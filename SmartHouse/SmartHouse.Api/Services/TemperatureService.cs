using SmartHouse.Api.Database;
using SmartHouse.Models.Models;

namespace SmartHouse.Api.Services
{
    public interface ITemperatureService : IData<Temperature>
    {

    }
    public class TemperatureService : Data<Temperature>, ITemperatureService
    {
        public TemperatureService(ApplicationDbContext context) : base(context)
        {

        }
    }
}
