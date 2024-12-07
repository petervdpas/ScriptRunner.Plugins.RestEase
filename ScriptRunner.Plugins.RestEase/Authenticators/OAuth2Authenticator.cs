using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using RestSharp;
using ScriptRunner.Plugins.RestEase.Interfaces;

namespace ScriptRunner.Plugins.RestEase.Authenticators;

/// <summary>
///     An authenticator that uses OAuth2 Client Credentials flows to get and manage access tokens.
/// </summary>
public class OAuth2Authenticator : IAuthenticator
{
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _scope;
    private readonly string _tokenUrl;
    private string? _accessToken;

    /// <summary>
    ///     Initializes a new instance of the <see cref="OAuth2Authenticator" /> class.
    /// </summary>
    /// <param name="tokenUrl">The URL to retrieve the OAuth2 token.</param>
    /// <param name="clientId">The client ID for OAuth2 authentication.</param>
    /// <param name="clientSecret">The client secret for OAuth2 authentication.</param>
    /// <param name="scope">The scope of the access token.</param>
    public OAuth2Authenticator(string tokenUrl, string clientId, string clientSecret, string scope)
    {
        _tokenUrl = tokenUrl;
        _clientId = clientId;
        _clientSecret = clientSecret;
        _scope = scope;
    }

    /// <summary>
    ///     Adds an OAuth2 Bearer token to the Authorization header of the specified HTTP request.
    /// </summary>
    /// <param name="request">The HTTP request to which the Authorization header will be added.</param>
    /// <returns>A task that represents the asynchronous authentication operation.</returns>
    /// <exception cref="Exception">Thrown when the token request fails or the token is null.</exception>
    public async Task AuthenticateAsync(RestRequest request)
    {
        if (string.IsNullOrEmpty(_accessToken))
        {
            var client = new HttpClient();
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = _tokenUrl,
                ClientId = _clientId,
                ClientSecret = _clientSecret,
                Scope = _scope
            });

            if (tokenResponse.IsError) throw new Exception($"Failed to get access token: {tokenResponse.Error}");

            _accessToken = tokenResponse.AccessToken;
        }

        if (_accessToken is null) throw new Exception("Access token is null after token retrieval.");

        request.AddHeader("Authorization", $"Bearer {_accessToken}");
    }
}