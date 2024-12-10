using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ScriptRunner.Plugins.Attributes;
using ScriptRunner.Plugins.RestEase.Interfaces;

namespace ScriptRunner.Plugins.RestEase;

/// <summary>
///     A plugin that registers and provides a restful client.
/// </summary>
/// <remarks>
///     This plugin demonstrates how to register a service with the host application's DI container.
/// </remarks>
[PluginMetadata(
    name: "RestEase Plugin",
    description: "A plugin that provides a RestEase RESTful client for ScriptRunner.",
    author: "Peter van de Pas",
    version: "1.0.2",
    pluginSystemVersion: "1.0.18",
    frameworkVersion: ".NET 8.0",
    services: ["IRestEase"])]
public class Plugin : BaseAsyncServicePlugin
{
    /// <summary>
    /// Gets the name of the plugin.
    /// </summary>
    public override string Name => "RestEase Plugin";

    /// <summary>
    /// Asynchronously initializes the plugin using the provided configuration.
    /// </summary>
    /// <param name="configuration">A dictionary containing configuration key-value pairs for the plugin.</param>
    public override async Task InitializeAsync(IDictionary<string, object> configuration)
    {
        // Simulate async initialization (e.g., loading settings or validating configurations)
        await Task.Delay(100);
        Console.WriteLine(configuration.TryGetValue("RestEaseKey", out var restEaseValue)
            ? $"RestEaseKey value: {restEaseValue}"
            : "RestEaseKey not found in configuration.");
    }
    
    /// <summary>
    /// Asynchronously registers the plugin's services into the application's dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to register services into.</param>
    public override async Task RegisterServicesAsync(IServiceCollection services)
    {
        // Simulate async service registration (e.g., initializing an external resource)
        await Task.Delay(50);
        services.AddSingleton<IRestEase, RestEase>();
    }
    
    /// <summary>
    /// Asynchronously executes the plugin's main functionality.
    /// </summary>
    public override async Task ExecuteAsync()
    {
        // Example execution logic
        await Task.Delay(50);
        Console.WriteLine("RestEase Plugin executed.");
    }
}