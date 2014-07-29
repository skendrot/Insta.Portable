using AsyncOAuth;
using Insta.Portable.Extensions;
using Insta.Portable.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Insta.Portable
{
    public partial class InstapaperClient
    {
        private const string BookmarksBaseUrl = BaseUrl + "/1.1/bookmarks";

        public async Task<BookmarksResponse> GetBookmarksAsync()
        {
            const string url = BookmarksBaseUrl + "/list";

            var parameters = new Dictionary<string, string>();

            var response = await GetResponse(url, parameters).ConfigureAwait(false);
            if (response.IsSuccessStatusCode == false) return new BookmarksResponse();

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return await json.DeserialiseAsync<BookmarksResponse>();
        }

        public async Task<string> GetBookmarkContentAsync(int bookmarkId)
        {
            string url = BookmarksBaseUrl + "/get_text";

            var parameters = new Dictionary<string, string>
            {
                {"bookmark_id", bookmarkId.ToString()}
            };
            var response = await GetResponse(url, parameters).ConfigureAwait(false);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<Bookmark> AddBookmarkAsync(string bookmarkUrl, string title = null, string description = null, string folderId = null)
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
            return await json.DeserialiseAsync<Bookmark>();
        }

        public Task<Bookmark> UpdateReadProgressAsync(int bookmarkId, float readPercentage)
        {
            const string url = BookmarksBaseUrl + "/update_read_progress";

            var epoch = new DateTime(1970, 1, 1);
            var timestamp = DateTime.UtcNow.Ticks - epoch.Ticks;
            var parameters = new Dictionary<string, string>
            {
                {"progress", readPercentage.ToString()},
                {"progress_timestamp", timestamp.ToString()}
            };

            return GetBookmarkAsync(url, bookmarkId, parameters);
        }

        public async Task<bool> DeleteBookmarkAsync(int bookmarkId)
        {
            const string url = BookmarksBaseUrl + "/delete";

            var parameters = new Dictionary<string, string>
            {
                {"bookmark_id", bookmarkId.ToString()}
            };
            var response = await GetResponse(url, parameters).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }

        public Task<Bookmark> StarBookmarkAsync(int bookmarkId)
        {
            const string url = BookmarksBaseUrl + "/star";

            return GetBookmarkAsync(url, bookmarkId);
        }
        public Task<Bookmark> UnstarBookmarkAsync(int bookmarkId)
        {
            const string url = BookmarksBaseUrl + "/unstar";

            return GetBookmarkAsync(url, bookmarkId);
        }

        public Task<Bookmark> ArchiveBookmarkAsync(int bookmarkId)
        {
            const string url = BookmarksBaseUrl + "/archive";

            return GetBookmarkAsync(url, bookmarkId);
        }

        public Task<Bookmark> UnarchiveBookmarkAsync(int bookmarkId)
        {
            const string url = BookmarksBaseUrl + "/unarchive";

            return GetBookmarkAsync(url, bookmarkId);
        }

        private async Task<Bookmark> GetBookmarkAsync(string url, int bookmarkId, IDictionary<string, string> additionalParameters = null)
        {
            var parameters = new Dictionary<string, string>
            {
                {"bookmark_id", bookmarkId.ToString()}
            };
            if (additionalParameters != null)
            {
                foreach (var additionalParameter in additionalParameters)
                {
                    parameters.Add(additionalParameter.Key, additionalParameter.Value);
                }
            }

            var response = await GetResponse(url, parameters).ConfigureAwait(false);
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return await json.DeserialiseAsync<Bookmark>();
        }

        private Task<HttpResponseMessage> GetResponse(string url, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            var client = new HttpClient(new OAuthMessageHandler(_consumerKey, _consumerSecret, AccessToken));
            var message = new HttpRequestMessage(HttpMethod.Post, url);
            message.Content = new FormUrlEncodedContent(parameters ?? new Dictionary<string, string>());
            return client.SendAsync(message);
        }
    }
}