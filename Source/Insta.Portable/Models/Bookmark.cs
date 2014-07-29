using Newtonsoft.Json;

namespace Insta.Portable.Models
{
    public class Bookmark
    {
        [JsonProperty("bookmark_id")]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string Hash { get; set; }

        [JsonProperty("private_source")]
        public string PrivateSource { get; set; }

        // TODO: time converter
        [JsonProperty("progress_timestamp")]
        public int ProgressTimestamp { get; set; }
        public int Time { get; set; }
        public double Progress { get; set; }
        
        // TODO: bool converter
        public string Starred { get; set; }
    }
}
