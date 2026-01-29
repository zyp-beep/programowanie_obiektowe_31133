namespace App;
using System.Text.Json.Serialization;
// komponenty ticket'a
public class Ticket
{
    public string Imei { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Opis { get; set; } = string.Empty;
    public string Status { get; set; } = "Przyjęto";
    public DateTime Created { get; set; } = DateTime.Now;
    public List<string> Comments { get; set; } = new();
    public string? Parts { get; set; }    
}