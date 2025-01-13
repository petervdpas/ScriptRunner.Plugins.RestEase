using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ScriptRunner.Plugins.Attributes;
using ScriptRunner.Plugins.Models;
using ScriptRunner.Plugins.RestEase.Interfaces;
using ScriptRunner.Plugins.Utilities;

namespace ScriptRunner.Plugins.RestEase;

/// <summary>
///     A plugin that registers and provides a restful client.
/// </summary>
/// <remarks>
///     This plugin demonstrates how to register a service with the host application's DI container.
/// </remarks>
[PluginMetadata(
    name: "RestEase",
    description: "A plugin that provides a easy RESTful client for ScriptRunner.",
    author: "Peter van de Pas",
    version: "1.0.0",
    pluginSystemVersion: PluginSystemConstants.CurrentPluginSystemVersion,
    frameworkVersion: PluginSystemConstants.CurrentFrameworkVersion,
    services: ["IRestEase"])]
public class Plugin : BaseAsyncServicePlugin
{
    /// <summary>
    /// Gets the name of the plugin.
    /// </summary>
    public override string Name => "RestEase";

    /// <summary>
    /// Asynchronously initializes the plugin using the provided configuration.
    /// </summary>
    /// <param name="configuration">A dictionary containing configuration key-value pairs for the plugin.</param>
    public override async Task InitializeAsync(IEnumerable<PluginSettingDefinition> configuration)
    {
        // Store settings into LocalStorage
        PluginSettingsHelper.StoreSettings(configuration);

        // Optionally display the settings
        PluginSettingsHelper.DisplayStoredSettings();
        
        await Task.CompletedTask;
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
        
        var storedSetting = PluginSettingsHelper.RetrieveSetting<string>("PluginName");
        Console.WriteLine($"Retrieved PluginName: {storedSetting}");
    }
}