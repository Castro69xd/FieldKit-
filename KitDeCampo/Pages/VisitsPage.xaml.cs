using KitDeCampo.Models;
using KitDeCampo.Services;

namespace KitDeCampo.Pages;

public partial class VisitsPage : ContentPage
{
    private readonly DataService dataService;

    public VisitsPage(DataService dataService)
    {
        InitializeComponent();
        this.dataService = dataService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadVisits();
    }

    private async Task LoadVisits()
    {
        var visits = await dataService.GetVisitasAsync();
        VisitsCollection.ItemsSource = visits;
    }

    private async void NewVisit_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NewVisitPage(dataService));
    }

    // Evento para abrir la visita seleccionada
    private async void OnVisitSelected(object sender, EventArgs e)
    {
        if (sender is Border border && border.BindingContext is Visite selectedVisit)
        {
            await Navigation.PushAsync(new TasksPage(dataService, selectedVisit));
        }
    }
}