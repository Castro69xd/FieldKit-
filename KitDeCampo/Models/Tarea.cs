namespace KitDeCampo.Models;

public class Tarea
{
    public string Descripcion { get; set; } = string.Empty;

    public bool IsDone { get; set; }

    public int Minutos { get; set; }
}