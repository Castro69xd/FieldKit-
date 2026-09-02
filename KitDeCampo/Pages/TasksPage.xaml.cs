using KitDeCampo.Models;
using KitDeCampo.Services;

namespace KitDeCampo.Pages;

public partial class TasksPage : ContentPage
{
    private readonly DataService dataService;
    private readonly Visite visit;

    public TasksPage(DataService dataService, Visite visit)
    {
        InitializeComponent();

        this.dataService = dataService;
        this.visit = visit;

        UpdateUI();
    }

    // Actualiza la información que aparece en pantalla (Bilingüe)
    private void UpdateUI()
    {
        CustomerLabel.Text = visit.NombreCliente;

        StatusLabel.Text = $"Status / Estado: {visit.Estado}";

        TimeLabel.Text = $"Total: {visit.TiempoTotal} min";

        TasksCollection.ItemsSource = null;
        TasksCollection.ItemsSource = visit.Tareas;
    }

    // Agregar una nueva tarea
    private async void AddTask_Clicked(object sender, EventArgs e)
    {
        // Verificar que haya una descripción
        if (string.IsNullOrWhiteSpace(TaskEntry.Text))
        {
            await DisplayAlert(
                "Missing task / Tarea requerida",
                "Please enter a task description / Ingrese la descripción de la tarea.",
                "OK");

            return;
        }

        // Convertir los minutos escritos a número
        int.TryParse(MinutesEntry.Text, out int minutes);

        // Crear la tarea
        visit.Tareas.Add(new Tarea
        {
            Descripcion = TaskEntry.Text,
            IsDone = false,
            Minutos = minutes
        });

        // Limpiar los campos
        TaskEntry.Text = "";
        MinutesEntry.Text = "";

        // Guardar cambios
        await Save();

        // Actualizar pantalla
        UpdateUI();
    }

    // Cuando se marca/desmarca una tarea
    private async void Task_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        await Save();
        UpdateUI();
    }

    // Guardar la visita en JSON
    private async Task Save()
    {
        var visits = await dataService.GetVisitasAsync();

        var existing = visits.FirstOrDefault(v => v.Id == visit.Id);

        if (existing != null)
        {
            existing.NombreCliente = visit.NombreCliente;
            existing.Fecha = visit.Fecha;
            existing.Tareas = visit.Tareas;
        }
        else
        {
            visits.Add(visit);
        }

        await dataService.SaveVisitasAsync(visits);
    }

    // Ir a la pantalla de resumen
    private async void Summary_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SummaryPage(dataService, visit));
    }
}