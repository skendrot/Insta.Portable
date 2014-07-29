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
        Task<BookmarksResponse> GetBookmarksAsync(int? limit = null, string folderType = null, IEnumerable<int> alreadyHave = null, IEnumerable<int> highlights = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<string> GetBookmarkContentAsync(int bookmarkId, CancellationToken cancellationToken = default(CancellationToken));
        Task<Bookmark> AddBookmarkAsync(string bookmarkUrl, string title = null, string description = null, string folderId = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<Bookmark> UpdateReadProgressAsync(int bookmarkId, float readPercentage, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> DeleteBookmarkAsync(int bookmarkId, CancellationToken cancellationToken = default(CancellationToken));
        Task<Bookmark> StarBookmarkAsync(int bookmarkId, CancellationToken cancellationToken = default(CancellationToken));
        Task<Bookmark> UnstarBookmarkAsync(int bookmarkId, CancellationToken cancellationToken = default(CancellationToken));
        Task<Bookmark> ArchiveBookmarkAsync(int bookmarkId, CancellationToken cancellationToken = default(CancellationToken));
        Task<Bookmark> UnarchiveBookmarkAsync(int bookmarkId, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<Folder>> GetFoldersAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<Folder> AddFolderAsync(string title, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> DeleteFolderAsync(string folderId, CancellationToken cancellationToken = default(CancellationToken));
        AccessToken AccessToken { get; set; }
    }
}
