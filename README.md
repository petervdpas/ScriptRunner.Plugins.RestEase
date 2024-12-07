# ScriptRunner.Plugins.RestEase

A powerful plugin for **ScriptRunner** designed to simplify RESTful API integrations. This plugin leverages RestSharp to enable seamless interaction with REST APIs, supporting dynamic authentication, flexible configurations, and easy-to-use methods for common HTTP operations.

---

## 🚀 Features

- **Dynamic Base URL Management**: Easily configure base URLs for different APIs.
- **Built-in Authentication**: Includes support for Basic, Bearer token, and OAuth2 authentications.
- **Extensibility**: Supports registration of custom authentication strategies.
- **Ease of Use**: Simplified methods for common HTTP operations: `GET`, `POST`, `PUT`, and `DELETE`.
- **ScriptRunner Integration**: Fully integrated into the ScriptRunner ecosystem.

---

## 📦 Installation

### Plugin Activation
Place the `ScriptRunner.Plugins.RestEase` plugin assembly in the `Plugins` folder of your ScriptRunner project. The plugin will be automatically discovered and activated.

---

## 📖 Usage

### Writing a Script

Here’s an example script for fetching cryptocurrency market data from the [CoinGecko API](https://www.coingecko.com/en/api):

```csharp
/*
{
    "TaskCategory": "Networking",
    "TaskName": "Fetch Cryptocurrency Market Data",
    "TaskDetail": "This script fetches cryptocurrency market data from CoinGecko."
}
*/

var restEase = PluginLoader.GetPlugin<ScriptRunner.Plugins.RestEase.IRestEase>();
restEase.SetBaseUrl("https://api.coingecko.com/api/v3");

var queryParams = new
{
    vs_currency = "usd",
    ids = "bitcoin,ethereum",
    order = "market_cap_desc",
    per_page = 10,
    page = 1,
    sparkline = false
};

var queryString = string.Join("&", queryParams.GetType().GetProperties()
    .Select(p => $"{p.Name}={Uri.EscapeDataString(p.GetValue(queryParams)?.ToString() ?? string.Empty)}"));

var response = await restEase.GetAsync($"/coins/markets?{queryString}");
Dump(response.Content.ReformatJson());

return "Cryptocurrency data fetched successfully.";
```

---

## 🔧 Configuration

### Initialize the Plugin
```csharp
var configuration = new Dictionary<string, object>
{
    { "DefaultTimeout", 30 } // Timeout in seconds
};

var restEase = PluginLoader.GetPlugin<ScriptRunner.Plugins.RestEase.IRestEase>();
restEase.Initialize(configuration);
```

### Authentication
Set the authentication for your requests:
```csharp
restEase.SetAuthentication("bearer", "your-bearer-token");
```

Supported authentication types:
- **none**: No authentication
- **basic**: Username and password as `(username, password)`
- **bearer**: Bearer token
- **oauth2**: OAuth2 with `(tokenUrl, clientId, clientSecret, scope)`

---

## 🌟 Advanced Features

### Custom Authenticator
Register a custom authenticator within your script or plugin:

```csharp
restEase.RegisterAuthenticator("customAuth", options => new CustomAuthenticator(options));
restEase.SetAuthentication("customAuth", customOptions);
```

---

## 🧪 Testing

- Use tools like [Postman](https://www.postman.com/) to validate the endpoints before scripting.
- Test end-to-end workflows within ScriptRunner using this plugin for your API tasks.

---

## 🛡 License

This plugin is licensed under the [MIT License](https://opensource.org/licenses/MIT).

---

## 📄 Contributing

1. Fork this repository.
2. Create a feature branch (`git checkout -b feature/YourFeature`).
3. Commit your changes (`git commit -m 'Add YourFeature'`).
4. Push the branch (`git push origin feature/YourFeature`).
5. Open a pull request.

---

## 🔗 Links

- [ScriptRunner Plugins Repository](https://github.com/petervdpas/ScriptRunner.Plugins)
- [RestSharp Documentation](https://restsharp.dev/)
