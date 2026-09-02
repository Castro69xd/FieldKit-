using System.Text.Json;
using KitDeCampo.Models;

namespace KitDeCampo.Services;

public class DataService
{
    private readonly string filePath;

    public DataService()
    {
        filePath = Path.Combine(
            FileSystem.AppDataDirectory,
            "visitas.json");
    }

    public async Task<List<Visite>> GetVisitasAsync()
    {
        if (!File.Exists(filePath))
            return new List<Visite>();

        string json = await File.ReadAllTextAsync(filePath);

        if (string.IsNullOrWhiteSpace(json))
            return new List<Visite>();

        return JsonSerializer.Deserialize<List<Visite>>(json)
               ?? new List<Visite>();
    }

    public async Task SaveVisitasAsync(List<Visite> visitas)
    {
        string json = JsonSerializer.Serialize(
            visitas,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        await File.WriteAllTextAsync(filePath, json);
    }
}