using KitDeCampo.Pages;

namespace KitDeCampo;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new NavigationPage(
            new VisitsPage(
                new Services.DataService()));
    }
}