using AsyncOAuth;
using Insta.Portable.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Insta.Portable
{
    public partial class InstapaperClient
    {
        private const string BookmarksBaseUrl = BaseUrl + "/1.1/bookmarks";

        public async Task<BookmarksResponse> GetBookmarks()
        {
            const string url = BookmarksBaseUrl + "/list";

            var parameters = new Dictionary<string, string>();

            var response = await GetResponse(url, parameters).ConfigureAwait(false);
            if (response.IsSuccessStatusCode == false) return new BookmarksResponse();

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<BookmarksResponse>(json);
        }

        public async Task<Bookmark> AddBookmark(string bookmarkUrl, string title = null, string description = null, string folderId = null)
        {
            if (bookmarkUrl == null) throw new ArgumentNullException("bookmarkUrl");

            const string url = BookmarksBaseUrl + "/add";

            var parameters = new Dictionary<string, string>
            {
                {"url", bookmarkUrl}
            };

            if (string.IsNullOrEmpty(title) == false)
            {
                parameters["title"] = title;
            }
            if (string.IsNullOrEmpty(description) == false)
            {
                parameters["description"] = description;
            }
            var response = await GetResponse(url, parameters).ConfigureAwait(false);
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<Bookmark>(json);
        }

        public async Task<Bookmark> UpdateReadProgress(int bookmarkId, float readPercentage)
        {
            const string url = BookmarksBaseUrl + "/update_read_progress";

            var epoch = new DateTime(1970, 1, 1);
            var timestamp = DateTime.UtcNow.Ticks - epoch.Ticks;
            var parameters = new Dictionary<string, string>
            {
                {"bookmark_id", bookmarkId.ToString()},
                {"progress", readPercentage.ToString()},
                {"progress_timestamp", timestamp.ToString()}
            };

            var response = await GetResponse(url, parameters).ConfigureAwait(false);
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<Bookmark>(json);
        }

        public async Task<bool> DeleteBookmark(string bookmarkId)
        {
            if (bookmarkId == null) throw new ArgumentNullException("bookmarkId");

            const string url = BookmarksBaseUrl + "/delete";

            var parameters = new Dictionary<string, string>
            {
                {"bookmark_id", bookmarkId}
            };
            var response = await GetResponse(url, parameters).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }

        private Task<HttpResponseMessage> GetResponse(string url, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            var client = new HttpClient(new OAuthMessageHandler(_consumerKey, _consumerSecret, AccessToken));
            var message = new HttpRequestMessage(HttpMethod.Post, url);
            message.Content = new FormUrlEncodedContent(parameters ?? new Dictionary<string,string>());
            return client.SendAsync(message);
        }
    }
}