using Flurl.Http;
using SmartHouse.Models;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartHouse.Mobile
{
    public class APIService
    {
        private readonly string _route;
        public APIService(string route)
        {
            _route = route;
        }

#if DEBUG
        private readonly string _apiUrl = "http://localhost:50821/api";
#endif
#if RELEASE
        private string _apiUrl = "http://localhost:50821/api"; 
#endif

        public async Task<T> Get<T>(object search = null)
        {
            try
            {
                string url;
                url = $"{_apiUrl}/{_route}";

                if (search != null)
                {
                    url += "?";
                    url += await search.ToQueryString();
                }

                return await url.GetJsonAsync<T>();
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.HttpStatus == System.Net.HttpStatusCode.InternalServerError)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Server error", "Try again");
                }
                throw;
            }
        }

    }
}
