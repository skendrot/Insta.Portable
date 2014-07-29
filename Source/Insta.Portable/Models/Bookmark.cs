using System.Diagnostics;
using Insta.Portable.Converters;
using Newtonsoft.Json;
using PropertyChanged;

namespace Insta.Portable.Models
{
    [ImplementPropertyChanged]
    [DebuggerDisplay("Id: {Id}, Title: {Title}")]
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

        [JsonProperty("progress_timestamp")]
        [JsonConverter(typeof(EpochDateTimeConverter))]
        public float ProgressTimestamp { get; set; }
        [JsonConverter(typeof(EpochDateTimeConverter))]
        public float Time { get; set; }
        public double Progress { get; set; }
        
        // TODO: bool converter
        public string Starred { get; set; }
    }
}
