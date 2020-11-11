using SmartHouse.Mobile.ViewModels;
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
    }
}