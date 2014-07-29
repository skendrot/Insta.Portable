using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Insta.Portable.Models
{
    public class Folder
    {        
        [JsonProperty("folder_id")]
        public int Id { get; set; }

        public string Title { get; set; }

        [JsonProperty("display_title")]
        public string DisplayTitle { get; set; }

        [JsonProperty("sync_to_mobile")]
        public int SyncToMobile { get; set; }

        public long Position { get; set; }

        public string Type { get; set; }

        public string Slug { get; set; }
    }
}
