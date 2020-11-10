using Microcharts;
using Microcharts.Forms;
using SkiaSharp;
using SmartHouse.Models;
using SmartHouse.Models.Requests;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        async Task Init()
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
                    TemperatureList.Clear();
                    foreach (var item in temperatures)
                    {
                        TemperatureList.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(ex.Message, "Something went wrong!", "OK");
            }

        }
        public ObservableCollection<Temperature> TemperatureList { get; set; } = new ObservableCollection<Temperature>();

        public static readonly List<ChartEntry> entries = new List<ChartEntry>()
        {
            //new ChartEntry(200)
            //{
            //    Color = SKColor.Parse("#ff80ff"),
            //    TextColor = SKColor.Parse("#ff80ff"),
            //    Label="January",
            //    ValueLabel="200"
            //},
            // new ChartEntry(400)
            //{
            //    Color = SKColor.Parse("#A8F4B4"),
            //    TextColor = SKColor.Parse("#A8F4B4"),
            //    Label="February",
            //    ValueLabel="400"
            //},
            //new ChartEntry(-100)
            //{
            //    Color = SKColor.Parse("#B7A8F4"),
            //    TextColor = SKColor.Parse("#B7A8F4"),
            //    Label="March",
            //    ValueLabel="-100"
            //}
        };
        public Chart ChartLine => new LineChart()
        { 
            Entries = entries,
            LabelTextSize = 45,
            LabelOrientation = Orientation.Horizontal
        };
    }
}
