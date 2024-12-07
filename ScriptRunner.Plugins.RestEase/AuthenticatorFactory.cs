using System;
using System.Collections.Generic;
using ScriptRunner.Plugins.RestEase.Authenticators;
using ScriptRunner.Plugins.RestEase.Interfaces;

namespace ScriptRunner.Plugins.RestEase;

/// <summary>
///     Factory class for creating instances of authenticators based on the specified authentication type.
/// </summary>
public static class AuthenticatorFactory
{
    private static readonly Dictionary<string, Func<object?, IAuthenticator>> AuthenticatorRegistry =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "none", _ => new NoAuthenticator() },
            { "bearer", options => CreateBearerTokenAuthenticator(options) },
            { "basic", options => CreateBasicAuthenticator(options) },
            { "oauth2", options => CreateOAuth2Authenticator(options) }
        };

    /// <summary>
    ///     Creates an authenticator instance based on the specified authentication type.
    /// </summary>
    /// <param name="authType">The type of authentication (e.g., "none", "bearer", "basic", "oauth2").</param>
    /// <param name="options">Optional parameters required for the specific authentication type.</param>
    /// <returns>An instance of <see cref="IAuthenticator" /> configured for the specified authentication type.</returns>
    /// <exception cref="ArgumentException">Thrown when an unsupported authentication type is specified or options are invalid.</exception>
    public static IAuthenticator CreateAuthenticator(string authType, object? options = null)
    {
        ArgumentNullException.ThrowIfNull(authType);

        if (AuthenticatorRegistry.TryGetValue(authType, out var factory)) return factory(options);

        throw new ArgumentException($"Unsupported authentication type: {authType}");
    }

    /// <summary>
    ///     Registers a custom authenticator for the factory.
    /// </summary>
    /// <param name="authType">The authentication type to register.</param>
    /// <param name="factory">A factory function to create the authenticator.</param>
    /// <exception cref="ArgumentException">Thrown if authType is null or already registered.</exception>
    public static void RegisterAuthenticator(string authType, Func<object?, IAuthenticator> factory)
    {
        if (string.IsNullOrWhiteSpace(authType)) throw new ArgumentException("Auth type cannot be null or empty.");
        if (!AuthenticatorRegistry.TryAdd(authType, factory))
            throw new ArgumentException($"Authenticator for type '{authType}' is already registered.");
    }

    /// <summary>
    ///     Creates a bearer token authenticator.
    /// </summary>
    /// <param name="options">The bearer token string.</param>
    /// <returns>An instance of <see cref="IAuthenticator" /> configured with the specified bearer token.</returns>
    /// <exception cref="ArgumentException">Thrown when options is not a valid bearer token string.</exception>
    private static BearerTokenAuthenticator CreateBearerTokenAuthenticator(object? options)
    {
        if (options is not string token || string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Bearer token requires a valid non-empty token string as options.");
        return new BearerTokenAuthenticator(token);
    }

    /// <summary>
    ///     Creates a basic authenticator.
    /// </summary>
    /// <param name="options">A tuple (username, password) for basic authentication.</param>
    /// <returns>An instance of <see cref="IAuthenticator" /> configured with the specified username and password.</returns>
    /// <exception cref="ArgumentException">Thrown when options is not a valid tuple (username, password).</exception>
    private static BasicAuthenticator CreateBasicAuthenticator(object? options)
    {
        if (options is not (string username, string password) || string.IsNullOrWhiteSpace(username) ||
            string.IsNullOrWhiteSpace(password))
            throw new ArgumentException(
                "Basic authentication requires a tuple (username, password) with non-empty values.");
        return new BasicAuthenticator(username, password);
    }

    /// <summary>
    ///     Creates an OAuth2 authenticator.
    /// </summary>
    /// <param name="options">
    ///     A tuple (tokenUrl, clientId, clientSecret, scope) for OAuth2 client credentials authentication:
    ///     - tokenUrl: The URL to retrieve the OAuth2 token.
    ///     - clientId: The client ID.
    ///     - clientSecret: The client secret.
    ///     - scope: The scope of the access token.
    /// </param>
    /// <returns>An instance of <see cref="IAuthenticator" /> configured for OAuth2 authentication.</returns>
    /// <exception cref="ArgumentException">Thrown when options is not a valid tuple (tokenUrl, clientId, clientSecret, scope).</exception>
    private static OAuth2Authenticator CreateOAuth2Authenticator(object? options)
    {
        if (options is not (string tokenUrl, string clientId, string clientSecret, string scope) ||
            string.IsNullOrWhiteSpace(tokenUrl) ||
            string.IsNullOrWhiteSpace(clientId) ||
            string.IsNullOrWhiteSpace(clientSecret) ||
            string.IsNullOrWhiteSpace(scope))
            throw new ArgumentException(
                "OAuth2 authentication requires a tuple (tokenUrl, clientId, clientSecret, scope) with valid non-empty values.");
        return new OAuth2Authenticator(tokenUrl, clientId, clientSecret, scope);
    }
}