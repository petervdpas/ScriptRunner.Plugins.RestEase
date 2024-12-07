using System.Collections.Generic;

namespace ScriptRunner.Plugins.RestEase.Models;

/// <summary>
///     Represents a response from a RESTful API request.
/// </summary>
public class RestEaseResponse
{
    /// <summary>
    ///     Gets or sets the HTTP status code returned by the API.
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    ///     Gets or sets the content of the response body.
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the API request was successful.
    /// </summary>
    public bool IsSuccess { get; set; }

    /// <summary>
    ///     Gets or sets the collection of headers included in the API response.
    /// </summary>
    public IDictionary<string, string>? Headers { get; set; }
}