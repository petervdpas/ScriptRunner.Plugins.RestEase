using System.Threading.Tasks;
using RestSharp;

namespace ScriptRunner.Plugins.RestEase.Interfaces;

/// <summary>
///     Represents an authenticator for adding authentication details to HTTP requests.
/// </summary>
public interface IAuthenticator
{
    /// <summary>
    ///     Authenticates the specified HTTP request by adding necessary authentication headers or parameters.
    /// </summary>
    /// <param name="request">The HTTP request to be authenticated.</param>
    /// <returns>A task that represents the asynchronous operation of authenticating the request.</returns>
    Task AuthenticateAsync(RestRequest request);
}