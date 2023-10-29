namespace CarRent.Database.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
