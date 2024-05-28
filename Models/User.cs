namespace GestiondeEventos.Models
{
    public class User
    {
        public int id { get; set; }

        public string name { get; set; } = null!;

        public string email { get; set; } = null!;

        public string password { get; set; } = null!;

        public string role { get; set; } = null!;

        public ICollection<Event> Events { get; set; } = new List<Event>();

        public ICollection<Notify> Notifications { get; set; } = new List<Notify>();


    }
}
