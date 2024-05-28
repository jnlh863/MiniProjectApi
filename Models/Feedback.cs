namespace GestiondeEventos.Models
{
    public class Feedback
    {
        public int id { get; set; }

        public int eventId { get; set; }

        public int userId { get; set; }

        public string rating { get; set; } = null!;

        public string comment { get; set; } = null!;

        public string date { get; set; } = null!;

    }
}
