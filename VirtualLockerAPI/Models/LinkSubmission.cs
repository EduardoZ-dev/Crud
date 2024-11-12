namespace VirtualLockerAPI.Models
{
    public class LinkSubmission
    {
        public int Id { get; set; } // Clave primaria
        public string Link { get; set; } = string.Empty;
        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }
}
