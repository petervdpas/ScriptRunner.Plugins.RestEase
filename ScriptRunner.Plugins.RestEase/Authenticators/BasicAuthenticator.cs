using System;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using ScriptRunner.Plugins.RestEase.Interfaces;

namespace ScriptRunner.Plugins.RestEase.Authenticators;

/// <summary>
///     An authenticator that uses Basic Authentication to add an Authorization header to HTTP requests.
/// </summary>
public class BasicAuthenticator : IAuthenticator
{
    private readonly string _password;
    private readonly string _username;

    /// <summary>
    ///     Initializes a new instance of the <see cref="BasicAuthenticator" /> class.
    /// </summary>
    /// <param name="username">The username for Basic Authentication.</param>
    /// <param name="password">The password for Basic Authentication.</param>
    public BasicAuthenticator(string username, string password)
    {
        _username = username;
        _password = password;
    }

    /// <summary>
    ///     Adds a Basic Authentication header to the specified HTTP request.
    /// </summary>
    /// <param name="request">The HTTP request to which the Authorization header will be added.</param>
    /// <returns>A completed <see cref="Task" />.</returns>
    public Task AuthenticateAsync(RestRequest request)
    {
        var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_username}:{_password}"));
        request.AddHeader("Authorization", $"Basic {credentials}");
        return Task.CompletedTask;
    }
}