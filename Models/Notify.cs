namespace GestiondeEventos.Models
{
    public class Notify
    {
        public int id { get; set; }

        public int userId { get; set; }
        public User User { get; set; } = null!;

        public int eventId { get; set; }
        public Event Event { get; set; } = null!;

        public string message { get; set; } = null!;

        public string date { get; set; } = null!;

    }
}
