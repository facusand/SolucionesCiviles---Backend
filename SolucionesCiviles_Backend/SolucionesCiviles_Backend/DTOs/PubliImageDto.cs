namespace SolucionesCiviles_Backend.DTOs
{
    public class PubliImageDto
    {
        public int Id { get; set; }
        public string? Filename { get; set; }
        public string? Descripcion { get; set; }
        public string? Path { get; set; }
        public IFormFileCollection? Image { get; set; }
        public List<ImageDto>? ImagesDto { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
