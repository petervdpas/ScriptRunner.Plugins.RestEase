using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using ScriptRunner.Plugins.RestEase.Authenticators;
using ScriptRunner.Plugins.RestEase.Interfaces;
using ScriptRunner.Plugins.RestEase.Models;

namespace ScriptRunner.Plugins.RestEase;

/// <summary>
///     Provides a REST client service for executing HTTP requests with optional dynamic authentication.
/// </summary>
public class RestEase : IRestEase
{
    private IAuthenticator _authenticator = new NoAuthenticator();
    private RestClient? _client;
    private RestClientOptions _options = new();

    /// <summary>
    ///     Registers a custom authenticator factory for a specific authentication type.
    /// </summary>
    /// <param name="authType">The type of authentication (e.g., "custom").</param>
    /// <param name="factory">A function that takes an optional object parameter and returns an IAuthenticator instance.</param>
    public void RegisterAuthenticator(string authType, Func<object?, IAuthenticator> factory)
    {
        AuthenticatorFactory.RegisterAuthenticator(authType, factory);
    }

    /// <summary>
    ///     Sets the base URL for the REST client.
    /// </summary>
    /// <param name="baseUrl">The base URL for the REST API.</param>
    /// <exception cref="ArgumentException">Thrown when the provided base URL is null or empty.</exception>
    public void SetBaseUrl(string baseUrl)
    {
        if (string.IsNullOrWhiteSpace(baseUrl)) throw new ArgumentException("Base URL cannot be null or empty.");
        _options.BaseUrl = new Uri(baseUrl);
        _client = new RestClient(_options);
    }

    /// <summary>
    ///     Configures the authentication type for the REST client.
    /// </summary>
    /// <param name="authType">The type of authentication (e.g., "bearer", "basic", "oauth2").</param>
    /// <param name="authOptions">Optional parameters required for the specified authentication type. Can be null.</param>
    public void SetAuthentication(string authType, object? authOptions = null)
    {
        if (authOptions != null) _authenticator = AuthenticatorFactory.CreateAuthenticator(authType, authOptions);
    }

    /// <summary>
    ///     Initializes the RestEase service with configuration settings.
    /// </summary>
    /// <param name="configuration">A dictionary containing configuration settings for the REST client.</param>
    public void Initialize(IDictionary<string, object> configuration)
    {
        if (configuration.TryGetValue("DefaultTimeout", out var timeout))
            if (int.TryParse(timeout.ToString(), out var timeoutSeconds))
            {
                _options = new RestClientOptions
                {
                    BaseUrl = _options.BaseUrl,
                    Timeout = TimeSpan.FromSeconds(timeoutSeconds)
                };
                _client = new RestClient(_options);
            }

        Console.WriteLine("RestEase initialized with configuration.");
    }

    /// <summary>
    ///     Executes a GET request to the specified resource.
    /// </summary>
    /// <param name="resource">The resource endpoint relative to the base URL.</param>
    /// <returns>The HTTP response from the GET request.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the base URL has not been set.</exception>
    public async Task<RestEaseResponse> GetAsync(string resource)
    {
        EnsureClientInitialized();
        var request = new RestRequest(resource);
        await AuthenticateRequestAsync(request);
        var response = await _client!.ExecuteAsync(request);
        return ConvertToRestEaseResponse(response);
    }

    /// <summary>
    ///     Executes a POST request to the specified resource with the provided body.
    /// </summary>
    /// <param name="resource">The resource endpoint relative to the base URL.</param>
    /// <param name="body">The data to be sent in the request body.</param>
    /// <returns>The HTTP response from the POST request.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the base URL has not been set.</exception>
    public async Task<RestEaseResponse> PostAsync(string resource, object body)
    {
        EnsureClientInitialized();
        var request = new RestRequest(resource, Method.Post);
        request.AddJsonBody(body);
        await AuthenticateRequestAsync(request);
        var response = await _client!.ExecuteAsync(request);
        return ConvertToRestEaseResponse(response);
    }

    /// <summary>
    ///     Executes a PUT request to the specified resource with the provided body.
    /// </summary>
    /// <param name="resource">The resource endpoint relative to the base URL.</param>
    /// <param name="body">The data to be sent in the request body.</param>
    /// <returns>The HTTP response from the PUT request.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the base URL has not been set.</exception>
    public async Task<RestEaseResponse> PutAsync(string resource, object body)
    {
        EnsureClientInitialized();
        var request = new RestRequest(resource, Method.Put);
        request.AddJsonBody(body);
        await AuthenticateRequestAsync(request);
        var response = await _client!.ExecuteAsync(request);
        return ConvertToRestEaseResponse(response);
    }

    /// <summary>
    ///     Executes a DELETE request to the specified resource.
    /// </summary>
    /// <param name="resource">The resource endpoint relative to the base URL.</param>
    /// <returns>The HTTP response from the DELETE request.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the base URL has not been set.</exception>
    public async Task<RestEaseResponse> DeleteAsync(string resource)
    {
        EnsureClientInitialized();
        var request = new RestRequest(resource, Method.Delete);
        await AuthenticateRequestAsync(request);
        var response = await _client!.ExecuteAsync(request);
        return ConvertToRestEaseResponse(response);
    }

    /// <summary>
    ///     Converts a RestSharp response to a RestEaseResponse.
    /// </summary>
    /// <param name="response">The RestSharp response to convert.</param>
    /// <returns>A RestEaseResponse instance with the mapped data.</returns>
    private static RestEaseResponse ConvertToRestEaseResponse(RestResponse response)
    {
        return new RestEaseResponse
        {
            StatusCode = (int)response.StatusCode,
            Content = response.Content ?? string.Empty,
            IsSuccess = response.IsSuccessful,
            Headers = response.Headers?
                .GroupBy(h => h.Name)
                .ToDictionary(
                    g => g.Key, 
                    g => string.Join(
                        ", ", g.Select(h => h.Value?.ToString() ?? string.Empty)) 
                ) ?? new Dictionary<string, string>()
        };
    }

    /// <summary>
    ///     Authenticates the HTTP request using the configured authenticator.
    /// </summary>
    /// <param name="request">The HTTP request to be authenticated.</param>
    private async Task AuthenticateRequestAsync(RestRequest request)
    {
        await _authenticator.AuthenticateAsync(request);
    }

    /// <summary>
    ///     Ensures the REST client has been initialized with a base URL.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the base URL is not set.</exception>
    private void EnsureClientInitialized()
    {
        if (_client == null)
            throw new InvalidOperationException(
                "Base URL has not been set. Call SetBaseUrl() before making requests.");
    }
}