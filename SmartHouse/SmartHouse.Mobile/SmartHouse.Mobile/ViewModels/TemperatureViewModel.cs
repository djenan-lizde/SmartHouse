using Microcharts;
using SkiaSharp;
using SmartHouse.Models;
using SmartHouse.Models.Requests;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartHouse.Mobile.ViewModels
{
    public enum Seasons { Spring, Summer, Autumn, Winter }
    public class TemperatureViewModel
    {
        private readonly APIService _apiServiceTemperatures = new APIService("Temperatures");
        public TemperatureViewModel() { FilterCommand = new Command(async () => await Init()); }

        public ICommand FilterCommand { get; set; }

        public DateTime DateTimeFrom { get; set; }
        public DateTime DateTimeTo { get; set; }

        public async Task Init()
        {
            try
            {
                var searchObject = new TemperatureSearchRequest();
                if (DateTimeFrom != null)
                {
                    searchObject.DateFrom = DateTimeFrom;
                }
                if (DateTimeTo != null)
                {
                    searchObject.DateTo = DateTimeTo;
                }
                var temperatures = await _apiServiceTemperatures.Get<List<Temperature>>(searchObject);
                if (temperatures.Count > 0)
                {
                    entries.Clear();
                    foreach (var item in temperatures)
                    {
                        entries.Add(new ChartEntry(item.TemperatureCelsius)
                        {
                            Color = SKColor.Parse("#000000"),
                            TextColor = SKColor.Parse("#a8f4b4"),
                            ValueLabel = item.TemperatureCelsius.ToString(),
                            Label = item.TemperatureCelsius.ToString(),
                            ValueLabelColor = SKColor.Parse("#FF0000")
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(ex.Message, "Something went wrong!", "OK");
            }

        }
        public static readonly List<ChartEntry> entries = new List<ChartEntry>();
        public Chart ChartLine => new LineChart()
        {
            Entries = entries,
            LabelTextSize = 45,
            LabelOrientation = Orientation.Horizontal,
            BackgroundColor = SKColors.Transparent,
            MinValue = 0,
            MaxValue = 50
        };
    }
}
