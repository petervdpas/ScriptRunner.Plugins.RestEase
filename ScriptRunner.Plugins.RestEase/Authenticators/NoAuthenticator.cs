using System.Threading.Tasks;
using RestSharp;
using ScriptRunner.Plugins.RestEase.Interfaces;

namespace ScriptRunner.Plugins.RestEase.Authenticators;

/// <summary>
///     An authenticator that performs no authentication.
/// </summary>
public class NoAuthenticator : IAuthenticator
{
    /// <summary>
    ///     Performs no authentication on the specified HTTP request.
    /// </summary>
    /// <param name="request">The HTTP request to be processed (no modifications will be made).</param>
    /// <returns>A completed <see cref="Task" /> indicating no authentication was performed.</returns>
    public Task AuthenticateAsync(RestRequest request)
    {
        // No authentication needed
        return Task.CompletedTask;
    }
}