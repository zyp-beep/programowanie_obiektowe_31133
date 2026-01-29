namespace App;
using System.Text.Json;
using System.Linq;

public static class TicketMgr
{
    private static readonly string ActiveFile = @"C:\Users\Dimisti\Documents\JetBrains Raider\Project\tickets.json";
    private static readonly string ArchiveFile = @"C:\Users\Dimisti\Documents\JetBrains Raider\Project\archive.json";

    static TicketMgr()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(ActiveFile));
        Directory.CreateDirectory(Path.GetDirectoryName(ArchiveFile));
    }
    
    public static List<Ticket> LoadActiveTickets()
    {
        if (!File.Exists(ActiveFile)) return new();
        string json = File.ReadAllText(ActiveFile);
        return JsonSerializer.Deserialize<List<Ticket>>(json) ?? new();
    }

    public static List<Ticket> LoadArchiveTickets()
    {
        if (!File.Exists(ArchiveFile)) return new();
        string json = File.ReadAllText(ArchiveFile);
        return JsonSerializer.Deserialize<List<Ticket>>(json) ?? new();
    }

    public static bool IsImeiTaken(string imei)
    {
        var tickets = LoadActiveTickets();
        return tickets.Any(t => t.Imei.Equals(imei, StringComparison.OrdinalIgnoreCase));
    }

    public static void AddTicket(Ticket ticket)
    {
        var tickets = LoadActiveTickets();
        tickets.Add(ticket);
        SaveActiveTickets(tickets);
    }

    public static Ticket? FindTicket(string imei)
    {
        var active = LoadActiveTickets();
        var ticket = active.FirstOrDefault(t => t.Imei.Equals(imei, StringComparison.OrdinalIgnoreCase));
        if (ticket != null) return ticket;

        var archive = LoadArchiveTickets();
        return archive.FirstOrDefault(t => t.Imei.Equals(imei, StringComparison.OrdinalIgnoreCase));
    }

    public static void ResolveTicket(string imei)
    {
        var active = LoadActiveTickets();
        var ticket = active.FirstOrDefault(t => t.Imei.Equals(imei, StringComparison.OrdinalIgnoreCase));
        if (ticket != null)
        {
            ticket.Status = "Zakończono";
            active.Remove(ticket);
            SaveActiveTickets(active);

            var archive = LoadArchiveTickets();
            archive.Add(ticket);
            SaveArchiveTickets(archive);
        }
    }

    private static void SaveActiveTickets(List<Ticket> tickets)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(tickets, options);
        File.WriteAllText(ActiveFile, json);
    }

    private static void SaveArchiveTickets(List<Ticket> tickets)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(tickets, options);
        File.WriteAllText(ArchiveFile, json);
    }
    //
    public static void ListAllActiveTickets()
    {
        var tickets = LoadActiveTickets();
        if (!tickets.Any())
        {
            Console.WriteLine("Brak zgłoszeń");
            return;
        }

        Console.WriteLine("\nAktywne zgłoszenia");
        foreach (var t in tickets)
        {
            Console.WriteLine($"IMEI: {t.Imei,-12} | {t.Model,-25} | Status: {t.Status,-12} | Utworzono: {t.Created:yyyy-MM-dd} \nOpis: {t.Opis, -12}");
        }
    }

    public static Ticket? GetActiveTicketByImei(string imei)
    {
        return LoadActiveTickets().FirstOrDefault(t => t.Imei.Equals(imei, StringComparison.OrdinalIgnoreCase));
    }

    public static void AddComment(string imei, string comment)
    {
        var ticket = GetActiveTicketByImei(imei);
        if (ticket != null)
        {
            ticket.Comments.Add($"[{DateTime.Now:yyyy-MM-dd HH:mm}] {comment}");
            SaveActiveTickets(LoadActiveTickets());
        }
    }

    public static void SetResolutionProcess(string imei, string process)
    {
        var ticket = GetActiveTicketByImei(imei);
        if (ticket != null)
        {
            ticket.Parts = process;
            ticket.Status = "Zakończono";
            ResolveTicket(imei);
        }
    }
    public static void ListArchiveTickets()
    {
        var tickets = LoadArchiveTickets();
        if (!tickets.Any())
        {
            Console.WriteLine("Brak zgłoszen w archiwum.");
            return;
        }

        Console.WriteLine("\nArchiwum");
        foreach (var t in tickets)
        {
            Console.WriteLine($"IMEI: {t.Imei,-12} | {t.Model,-25} | Status: {t.Status,-12} | Utworzono: {t.Created:yyyy-MM-dd}");
            if (!string.IsNullOrEmpty(t.Parts))
                Console.WriteLine($"  Urzyte częsći: {t.Parts}");
        }
    }}