namespace CarRent.Database.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? PhoteImageName { get; set; }
        public string? PhoteInFileSystem { get; set; }
        public List<Car> Cars { get; set; }
        
    }
}
