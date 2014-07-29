using Newtonsoft.Json;
using System.Collections.Generic;

namespace Insta.Portable.Models
{
    public class BookmarksResponse
    {
        public User User { get; set; }

        public IEnumerable<Bookmark> Bookmarks { get; set; }

        public IEnumerable<Bookmark> Highlights { get; set; }

        [JsonProperty("delete_ids")]
        public IEnumerable<int> DeletedIds { get; set; }
    }
}
