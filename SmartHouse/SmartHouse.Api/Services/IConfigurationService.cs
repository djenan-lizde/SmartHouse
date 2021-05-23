using SmartHouse.Api.Database;

namespace SmartHouse.Api.Services
{
    public interface IConfigurationService : IBaseService<Models.Models.Configuration>
    {
    }

    public class ConfigurationService :  BaseService<Models.Models.Configuration>,IConfigurationService
    {
        public ConfigurationService(ApplicationDbContext context): base(context)
        {

        }
    }
}
