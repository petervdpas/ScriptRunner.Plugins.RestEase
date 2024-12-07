using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ScriptRunner.Plugins.RestEase.Models;

namespace ScriptRunner.Plugins.RestEase.Interfaces;

/// <summary>
///     Interface for a REST client service that supports configurable base URL, authentication, and HTTP methods.
/// </summary>
public interface IRestEase
{
    /// <summary>
    ///     Registers a custom authenticator factory for a specific authentication type.
    /// </summary>
    /// <param name="authType">The type of authentication (e.g., "custom").</param>
    /// <param name="factory">A function that takes an optional object parameter and returns an IAuthenticator instance.</param>
    void RegisterAuthenticator(string authType, Func<object?, IAuthenticator> factory);

    /// <summary>
    ///     Sets the base URL for the REST client.
    /// </summary>
    /// <param name="baseUrl">The base URL for the REST API.</param>
    void SetBaseUrl(string baseUrl);

    /// <summary>
    ///     Configures the authentication type for the REST client.
    /// </summary>
    /// <param name="authType">The type of authentication (e.g., "bearer", "basic", "oauth2").</param>
    /// <param name="authOptions">Optional parameters required for the specified authentication type. Can be null.</param>
    void SetAuthentication(string authType, object? authOptions = null);

    /// <summary>
    ///     Initializes the REST client with the specified configuration settings.
    /// </summary>
    /// <param name="configuration">
    ///     A dictionary containing key-value pairs for configuration settings, such as timeouts or default headers.
    /// </param>
    void Initialize(IDictionary<string, object> configuration);

    /// <summary>
    ///     Executes a GET request to the specified resource.
    /// </summary>
    /// <param name="resource">The resource endpoint.</param>
    /// <returns>A task representing the asynchronous operation, returning a <see cref="RestEaseResponse" />.</returns>
    Task<RestEaseResponse> GetAsync(string resource);

    /// <summary>
    ///     Executes a POST request to the specified resource with a body.
    /// </summary>
    /// <param name="resource">The resource endpoint.</param>
    /// <param name="body">The body of the request.</param>
    /// <returns>A task representing the asynchronous operation, returning a <see cref="RestEaseResponse" />.</returns>
    Task<RestEaseResponse> PostAsync(string resource, object body);

    /// <summary>
    ///     Executes a PUT request to the specified resource with a body.
    /// </summary>
    /// <param name="resource">The resource endpoint.</param>
    /// <param name="body">The body of the request.</param>
    /// <returns>A task representing the asynchronous operation, returning a <see cref="RestEaseResponse" />.</returns>
    Task<RestEaseResponse> PutAsync(string resource, object body);

    /// <summary>
    ///     Executes a DELETE request to the specified resource.
    /// </summary>
    /// <param name="resource">The resource endpoint.</param>
    /// <returns>A task representing the asynchronous operation, returning a <see cref="RestEaseResponse" />.</returns>
    Task<RestEaseResponse> DeleteAsync(string resource);
}