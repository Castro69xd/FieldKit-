using KitDeCampo.Pages;
using KitDeCampo.Services;

namespace KitDeCampo;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        var navPage = new NavigationPage(new VisitsPage(new DataService()))
        {
            BarBackgroundColor = Color.FromArgb("#200508"),
            BarTextColor = Color.FromArgb("#D4AF37")
        };

        MainPage = navPage;
    }
}