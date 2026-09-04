using KitDeCampo.Models;
using KitDeCampo.Services;

namespace KitDeCampo.Pages;

public partial class SummaryPage : ContentPage
{
    private readonly Visite visit;
    private Editor? _summaryEditor;

    public SummaryPage(DataService dataService, Visite visit)
    {
        InitializeComponent();
        // Resuelve el acceso al control XAML sin depender del campo generado
        _summaryEditor = this.FindByName<Editor>("SummaryEditor");
        this.visit = visit;
        if (_summaryEditor != null)
            _summaryEditor.Text = GenerateSummary();
    }

    private string GenerateSummary()
    {
        var lines = new List<string>
        {
            "==================================",
            "       SERVICE VISIT SUMMARY      ",
            "==================================",
            "",
            $"Customer: {visit.NombreCliente}",
            $"Date:     {visit.Fecha:MMMM dd, yyyy}",
            $"Status:   {visit.Estado}",
            $"Total:    {visit.TiempoTotal} min",
            "",
            "----------------------------------",
            "Tasks Performed:",
            "----------------------------------"
        };

        if (visit.Tareas != null && visit.Tareas.Count > 0)
        {
            foreach (var task in visit.Tareas)
            {
                string status = task.IsDone ? "[X] Completed" : "[ ] Pending";
                lines.Add($"{status} - {task.Descripcion} ({task.Minutos} min)");
            }
        }
        else
        {
            lines.Add("No tasks recorded.");
        }

        lines.Add("==================================");

        return string.Join(Environment.NewLine, lines);
    }

    private async void CopySummary_Clicked(object sender, EventArgs e)
    {
        var text = _summaryEditor?.Text ?? string.Empty;
        await Clipboard.Default.SetTextAsync(text);
        await DisplayAlert("Success", "Summary copied to clipboard.", "OK");
    }

    private async void ShareSummary_Clicked(object sender, EventArgs e)
    {
        var text = _summaryEditor?.Text ?? string.Empty;
        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Text = text,
            Title = "Service Visit Summary"
        });
    }
}