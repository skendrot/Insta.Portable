using AsyncOAuth;
using Insta.Portable.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Insta.Portable
{
    public interface IInstapaperClient
    {
        Task<AccessToken> GetAuthTokenAsync(string userName, string password, CancellationToken cancellationToken = default(CancellationToken));
        Task<User> VerifyUserAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<BookmarksResponse> GetBookmarks();
        Task<string> GetBookmarkContent(int bookmarkId);
        Task<Bookmark> AddBookmark(string bookmarkUrl, string title = null, string description = null, string folderId = null);
        Task<Bookmark> UpdateReadProgress(int bookmarkId, float readPercentage);
        Task<bool> DeleteBookmark(int bookmarkId);
        Task<Bookmark> StarBookmark(int bookmarkId);
        Task<Bookmark> UnstarBookmark(int bookmarkId);
        Task<Bookmark> ArchiveBookmark(int bookmarkId);
        Task<Bookmark> UnarchiveBookmark(int bookmarkId);
        Task<IEnumerable<Folder>> GetFolders();
        Task<Folder> AddFolder(string title);
        Task<bool> DeleteFolder(string folderId);
        AccessToken AccessToken { get; set; }
    }
}
