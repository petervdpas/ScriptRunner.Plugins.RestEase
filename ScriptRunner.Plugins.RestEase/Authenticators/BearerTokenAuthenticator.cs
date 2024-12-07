using System.Threading.Tasks;
using RestSharp;
using ScriptRunner.Plugins.RestEase.Interfaces;

namespace ScriptRunner.Plugins.RestEase.Authenticators;

/// <summary>
///     An authenticator that adds a Bearer token to the Authorization header of HTTP requests.
/// </summary>
public class BearerTokenAuthenticator : IAuthenticator
{
    private readonly string _token;

    /// <summary>
    ///     Initializes a new instance of the <see cref="BearerTokenAuthenticator" /> class.
    /// </summary>
    /// <param name="token">The Bearer token to be used for authentication.</param>
    public BearerTokenAuthenticator(string token)
    {
        _token = token;
    }

    /// <summary>
    ///     Adds the Bearer token to the Authorization header of the specified HTTP request.
    /// </summary>
    /// <param name="request">The HTTP request to which the Authorization header will be added.</param>
    /// <returns>A completed <see cref="Task" />.</returns>
    public Task AuthenticateAsync(RestRequest request)
    {
        request.AddHeader("Authorization", $"Bearer {_token}");
        return Task.CompletedTask;
    }
}