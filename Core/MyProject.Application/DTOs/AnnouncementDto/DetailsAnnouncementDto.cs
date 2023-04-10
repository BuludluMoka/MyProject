namespace MyProject.Application.DTOs

{
    public class DetailsAnnouncementDto
    {
        public long Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public int Year { get; set; }
        public string Currencie { get; set; }
        public string BanType { get; set; }
        public DateTime RelaseDate { get; set; }
        public string OwnerContact { get; set; }
        public List<string> CarImageFiles { get; set; }
    }
}
