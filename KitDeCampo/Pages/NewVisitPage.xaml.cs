using KitDeCampo.Models;
using KitDeCampo.Services;

namespace KitDeCampo.Pages;

public partial class NewVisitPage : ContentPage
{
    private readonly DataService dataService;

    public NewVisitPage(DataService dataService)
    {
        InitializeComponent();
        this.dataService = dataService;
        VisitDatePicker.Date = DateTime.Today;
    }

    private async void CreateVisit_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(CustomerEntry.Text))
        {
            await DisplayAlert(
                "Error / Error",
                "Please enter the customer name / Por favor ingrese el nombre del cliente.",
                "OK");

            return;
        }

        var visits = await dataService.GetVisitasAsync();

        var visit = new Visite
        {
            Id = visits.Count == 0
                ? 1
                : visits.Max(v => v.Id) + 1,

            NombreCliente = CustomerEntry.Text,
            Fecha = VisitDatePicker.Date ?? DateTime.Today,
            Tareas = new List<Tarea>()
        };

        visits.Add(visit);
        await dataService.SaveVisitasAsync(visits);

        await Navigation.PushAsync(new TasksPage(dataService, visit));
    }
}