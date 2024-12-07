namespace ScriptRunner.Plugins.RestEase.Enums;

/// <summary>
///     Specifies the types of authentication supported by the REST client.
/// </summary>
public enum AuthenticationType
{
    /// <summary>
    ///     No authentication is required.
    /// </summary>
    None,

    /// <summary>
    ///     Bearer token authentication (e.g., OAuth2 access token).
    /// </summary>
    BearerToken,

    /// <summary>
    ///     API key authentication, typically passed in headers or query parameters.
    /// </summary>
    ApiKey,

    /// <summary>
    ///     Basic authentication using a username and password.
    /// </summary>
    Basic
}