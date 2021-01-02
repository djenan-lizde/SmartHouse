using SmartHouse.Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartHouse.Mobile.ViewModels
{
    public class TemperatureViewModel : BaseViewModel
    {
        private readonly APIService _apiServiceTemperatures = new APIService("Temperatures");
        public TemperatureViewModel()
        {
            CurrentTempCommand = new Command(async () => await CurrentTemp());
            DateTimeFrom = DateTime.Now;
        }

        public ICommand CurrentTempCommand { get; set; }

        public DateTime DateTimeFrom { get; set; }

        string temperature = string.Empty;
        public string Temperature
        {
            get { return temperature; }
            set { SetProperty(ref temperature, value); }
        }

        public async Task CurrentTemp()
        {
            try
            {
                var temp = await _apiServiceTemperatures.Get<Temperature>(null, "CurrentTemperature");
                Temperature = $"{temp.TemperatureCelsius}°C - {temp.TemperatureFahrenheit}°F - {temp.Humidity}% - " +
                    $"{temp.DateAdded}";
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(ex.Message, "Something went wrong!", "OK");
            }
        }

        public async Task Init(int day = 1, int month = 1, int year = 2018)
        {
            try
            {
                Temperatures.Clear();
                if (DateTimeFrom > DateTime.Now)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Something went wrong!", "OK");
                    return;
                }
                var temperatures = await _apiServiceTemperatures.Get<List<Temperature>>();

                if (DateTimeFrom != null)
                {
                    temperatures = temperatures.Where(x => x.DateAdded.Day == DateTimeFrom.Day
                             && x.DateAdded.Month == DateTimeFrom.Month && x.DateAdded.Year == DateTimeFrom.Year).ToList();
                }

                if (temperatures.Count > 0)
                {
                    foreach (var item in temperatures)
                    {
                        Temperatures.Add(new Temperature
                        {
                            DateAdded = item.DateAdded,
                            HeatIndex = item.HeatIndex,
                            Humidity = item.Humidity,
                            Id = item.Id,
                            TemperatureCelsius = item.TemperatureCelsius,
                            TemperatureFahrenheit = item.TemperatureFahrenheit
                        });
                    };
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(ex.Message, "Something went wrong!", "OK");
            }
        }

        public ObservableCollection<Temperature> Temperatures { get; set; } = new ObservableCollection<Temperature>();

    }
}
