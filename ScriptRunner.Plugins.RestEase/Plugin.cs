using System;
using System.Collections.Generic;
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
    "RestEase Plugin",
    "The RestEase Restful Client Plugin for ScriptRunner.",
    "Peter van de Pas",
    "1.0.0",
    "net8.0",
    ["IRestEase"])]
public class Plugin : IServicePlugin
{
    /// <summary>
    ///     Gets the name of the plugin.
    /// </summary>
    public string Name => "RestEase Plugin";

    /// <summary>
    ///     Initializes the plugin with the provided configuration.
    /// </summary>
    /// <param name="configuration">A dictionary containing key-value pairs for the plugin's configuration.</param>
    public void Initialize(IDictionary<string, object> configuration)
    {
        Console.WriteLine(configuration.TryGetValue("RestEaseKey", out var restEaseValue)
            ? $"RestEaseKey value: {restEaseValue}"
            : "RestEaseKey not found in configuration.");
    }

    /// <summary>
    ///     Executes the plugin's main functionality.
    /// </summary>
    public void Execute()
    {
        Console.WriteLine("RestEase Plugin executed.");
    }

    /// <summary>
    ///     Registers the plugin's services into the application's dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to register the plugin's services into.</param>
    public void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<IRestEase, RestEase>();
    }
}