namespace YouTubeApiProject.Models
{
    public class YouTubeVideoModel
    {
        public string VideoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Publisher { get; set; } // New property for publisher
    }
}
