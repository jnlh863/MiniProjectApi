namespace GestiondeEventos.Models
{
    public class Event
    {
        public int id { get; set; }

        public string title { get; set; } = null!;

        public string description { get; set; } = null!;

        public string date { get; set; } = null!;

        public string location { get; set; } = null!;

        public int organizerId { get; set; }

        public User User { get; set; } = null!;
        public ICollection<Notify> Notifications { get; set; } = new List<Notify>();

    }
}
