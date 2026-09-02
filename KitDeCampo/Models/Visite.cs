namespace KitDeCampo.Models;

public class Visite
{
    public int Id { get; set; }

    public string NombreCliente { get; set; } = string.Empty;

    public DateTime Fecha { get; set; } = DateTime.Now;

    public List<Tarea> Tareas { get; set; } = new();

    public string Estado
    {
        get
        {
            if (Tareas.Count == 0)
                return "Open";

            if (Tareas.All(t => t.IsDone))
                return "Closed";

            return "In Progress";
        }
    }

    public int TiempoTotal
    {
        get
        {
            return Tareas.Sum(t => t.Minutos);
        }
    }
}