using Insta.Portable.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insta.Portable
{
    public partial class InstapaperClient
    {
        private const string FoldersUrl = BaseUrl + "/1.1/folders";

        public async Task<IEnumerable<Folder>> GetFolders()
        {
            const string url = FoldersUrl + "/list";

            var response = await GetResponse(url, null).ConfigureAwait(false);
            if (response.IsSuccessStatusCode == false) return new Folder[0];

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<IEnumerable<Folder>>(json);
        }

        public async Task<Folder> AddFolder(string title)
        {
            if (title == null) throw new ArgumentNullException("title");

            const string url = FoldersUrl + "/add";

            var parameters = new Dictionary<string, string>
            {
                {"title", title}
            };

            var response = await GetResponse(url, parameters).ConfigureAwait(false);
            if (response.IsSuccessStatusCode == false) return null;

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<Folder>(json);
        }

        public async Task<bool> DeleteFolder(string folderId)
        {
            if (folderId == null) throw new ArgumentNullException("folderId");

            const string url = FoldersUrl + "/delete";

            var parameters = new Dictionary<string, string>
            {
                {"folder_id", folderId}
            };

            var response = await GetResponse(url, parameters).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }
    }
}
