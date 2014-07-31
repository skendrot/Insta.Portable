using Newtonsoft.Json;

namespace Insta.Portable.Models
{
    public class Error
    {
        [JsonProperty("error_code")]
        public int ErrorCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}