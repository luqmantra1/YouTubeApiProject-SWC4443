using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using YouTubeApiProject.Models;

namespace YouTubeApiProject.Services
{
    public class YouTubeApiService
    {
        private readonly string _apiKey;
        public YouTubeApiService(IConfiguration configuration)
        {
            _apiKey = configuration["YouTubeApiKey"];
        }

        public async Task<List<YouTubeVideoModel>> SearchVideosAsync(string query)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = _apiKey,
                ApplicationName = "YouTubeApiProject"
            });

            // Search for videos
            var searchRequest = youtubeService.Search.List("snippet");
            searchRequest.Q = query;
            searchRequest.MaxResults = 10;
            var searchResponse = await searchRequest.ExecuteAsync();

            var videoIds = searchResponse.Items.Select(item => item.Id.VideoId).ToList();

            // Now, fetch additional details for the found videos
            var videosRequest = youtubeService.Videos.List("snippet,statistics");
            videosRequest.Id = string.Join(",", videoIds);
            var videosResponse = await videosRequest.ExecuteAsync();

            var videos = videosResponse.Items.Select(item => new YouTubeVideoModel
            {
                VideoId = item.Id,
                Title = item.Snippet.Title,
                Description = item.Snippet.Description,
                ThumbnailUrl = item.Snippet.Thumbnails.Medium.Url,
                Publisher = item.Snippet.ChannelTitle, // Get channel name
            }).ToList();

            return videos;
        }

    }
}
