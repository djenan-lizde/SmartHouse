using SmartHouse.Mobile.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHouse.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TemperaturesPage : ContentPage
    {
        public TemperatureViewModel model = null;
        public TemperaturesPage()
        {
            InitializeComponent();
            BindingContext = model = new TemperatureViewModel();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await model.Init();
        }

        private async void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            var item = e.NewDate;
            await model.Init(item.Day, item.Month, item.Year);
        }
    }
}