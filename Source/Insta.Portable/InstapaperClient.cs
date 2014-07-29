using AsyncOAuth;
using PCLCrypto;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Insta.Portable
{
    public partial class InstapaperClient
    {
        private readonly string _consumerKey;
        private readonly string _consumerSecret;
        private const string BaseUrl = "https://www.instapaper.com/api";
        private const string AuthUrl = BaseUrl + "/1.1/oauth";
        private const string ProfileUrl = BaseUrl + "users/_current";

        private InstapaperClient()
        {
            OAuthUtility.ComputeHash = (key, buffer) =>
            {
                var crypt = WinRTCrypto.MacAlgorithmProvider.OpenAlgorithm(MacAlgorithm.HmacSha1);
                var keyBuffer = WinRTCrypto.CryptographicBuffer.CreateFromByteArray(key);
                var cryptKey = crypt.CreateKey(keyBuffer);

                var dataBuffer = WinRTCrypto.CryptographicBuffer.CreateFromByteArray(buffer);
                var signBuffer = WinRTCrypto.CryptographicEngine.Sign(cryptKey, dataBuffer);

                byte[] value;
                WinRTCrypto.CryptographicBuffer.CopyToByteArray(signBuffer, out value);
                return value;
            };
        }

        public InstapaperClient(string consumerKey, string consumerSecret, string oauthToken = null, string oauthSecret = null)
            : this()
        {
            _consumerKey = consumerKey;
            _consumerSecret = consumerSecret;
        }

        public AccessToken AccessToken { get; set; }

        public async Task<AccessToken> GetAuthTokenAsync(string userName, string password, CancellationToken cancellationToken = default(CancellationToken))
        {
            //
            // Acquiring a request token
            //

            const string authUrl = AuthUrl + "/access_token";

            var parameters = new Dictionary<string, string>
            {
                { "x_auth_username", userName },
                {"x_auth_password", password},
                {"x_auth_mode","client_auth"}
            };

            var handler = new OAuthMessageHandler(_consumerKey, _consumerSecret);
            var client = new HttpClient(handler);

            var response = await client.PostAsync(authUrl, new FormUrlEncodedContent(parameters), cancellationToken).ConfigureAwait(false);
            var tokenBase = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new HttpRequestException(response.StatusCode + ":" + tokenBase); // error message
            }

            var splitted = tokenBase.Split('&').Select(s => s.Split('=')).ToLookup(xs => xs[0], xs => xs[1]);
            AccessToken = new AccessToken(splitted["oauth_token"].First(), splitted["oauth_token_secret"].First());
            return AccessToken;
        }


    }
}
