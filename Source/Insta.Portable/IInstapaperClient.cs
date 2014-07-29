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

        Task<BookmarksResponse> GetBookmarks();
        Task<Bookmark> AddBookmark(string bookmarkUrl, string title = null, string description = null, string folderId = null);
        Task<Bookmark> UpdateReadProgress(int bookmarkId, float readPercentage);
        Task<bool> DeleteBookmark(string bookmarkId);
        
        Task<IEnumerable<Folder>> GetFolders();
        Task<Folder> AddFolder(string title);
        Task<bool> DeleteFolder(string folderId);
    }
}
