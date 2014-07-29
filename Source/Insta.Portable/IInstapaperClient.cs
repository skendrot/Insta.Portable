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
        Task<InstaResponse<User>> VerifyUserAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<InstaResponse<BookmarksResponse>> GetBookmarksAsync(int? limit = null, string folderType = null, IEnumerable<int> alreadyHave = null, IEnumerable<int> highlights = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<string> GetBookmarkContentAsync(int bookmarkId, CancellationToken cancellationToken = default(CancellationToken));
        Task<InstaResponse<Bookmark>> AddBookmarkAsync(string bookmarkUrl, string title = null, string description = null, string folderId = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<InstaResponse<Bookmark>> UpdateReadProgressAsync(int bookmarkId, float readPercentage, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> DeleteBookmarkAsync(int bookmarkId, CancellationToken cancellationToken = default(CancellationToken));
        Task<InstaResponse<Bookmark>> StarBookmarkAsync(int bookmarkId, CancellationToken cancellationToken = default(CancellationToken));
        Task<InstaResponse<Bookmark>> UnstarBookmarkAsync(int bookmarkId, CancellationToken cancellationToken = default(CancellationToken));
        Task<InstaResponse<Bookmark>> ArchiveBookmarkAsync(int bookmarkId, CancellationToken cancellationToken = default(CancellationToken));
        Task<InstaResponse<Bookmark>> UnarchiveBookmarkAsync(int bookmarkId, CancellationToken cancellationToken = default(CancellationToken));
        Task<InstaResponse<IEnumerable<Folder>>> GetFoldersAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<InstaResponse<Folder>> AddFolderAsync(string title, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> DeleteFolderAsync(string folderId, CancellationToken cancellationToken = default(CancellationToken));
        AccessToken AccessToken { get; set; }
    }
}
