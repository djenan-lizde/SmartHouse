using SmartHouse.Api.Database;
using SmartHouse.Models.Models;

namespace SmartHouse.Api.Services
{
    public interface ITemperatureService : IBaseService<Temperature>
    {

    }
    public class TemperatureService : BaseService<Temperature>, ITemperatureService
    {
        public TemperatureService(ApplicationDbContext context) : base(context)
        {

        }
    }
}
