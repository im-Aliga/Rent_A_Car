namespace CarRent.Database.Models
{
    public class Brand
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string? PhoteImageName { get; set; }
        public string? PhoteInFileSystem { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
