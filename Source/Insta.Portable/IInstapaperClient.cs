using AsyncOAuth;
using Insta.Portable.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Insta.Portable
{
    public interface IInstapaperClient
    {
        Task<AccessToken> GetAuthTokenAsync(string emailAddress, string password, CancellationToken cancellationToken = default(CancellationToken));
        Task<User> VerifyUserAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<BookmarksResponse> GetBookmarksAsync();
        Task<string> GetBookmarkContentAsync(int bookmarkId);
        Task<Bookmark> AddBookmarkAsync(string bookmarkUrl, string title = null, string description = null, string folderId = null);
        Task<Bookmark> UpdateReadProgressAsync(int bookmarkId, float readPercentage);
        Task<bool> DeleteBookmarkAsync(int bookmarkId);
        Task<Bookmark> StarBookmarkAsync(int bookmarkId);
        Task<Bookmark> UnstarBookmarkAsync(int bookmarkId);
        Task<Bookmark> ArchiveBookmarkAsync(int bookmarkId);
        Task<Bookmark> UnarchiveBookmarkAsync(int bookmarkId);
        Task<IEnumerable<Folder>> GetFoldersAsync();
        Task<Folder> AddFolderAsync(string title);
        Task<bool> DeleteFolderAsync(string folderId);
        AccessToken AccessToken { get; set; }
    }
}
