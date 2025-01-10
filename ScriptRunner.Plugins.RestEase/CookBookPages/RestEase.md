---
Title: The RestEase Restful Client
Subtitle: Fetch Cryptocurrency Market Data from CoinGecko Using RestEase Client
Category: Cookbook
Author: Peter van de Pas
keywords: [CookBook, RestEase, CoinGecko]
table-use-row-colors: true
table-row-color: "D3D3D3"
toc: true
toc-title: Table of Content
toc-own-page: true
---

# Recipe: Fetch Cryptocurrency Market Data from CoinGecko Using RestEase Client

## Goal

Learn how to leverage the **RestEase Client** plugin in ScriptRunner to query CoinGecko's API for cryptocurrency market data efficiently.

## Overview

This recipe demonstrates how to:
1. Configure the RestEase client with a base URL.
2. Dynamically build a query string.
3. Execute an HTTP GET request to fetch cryptocurrency data.
4. Format and display the API response.

By the end of this tutorial, you'll have a working script that fetches cryptocurrency data using CoinGecko's public API.

---

## Steps

### 1. Define Task Metadata

Each script in ScriptRunner includes metadata for categorization and identification. Add the following JSON metadata to your script:

```csharp
/*
{
    "TaskCategory": "Networking",
    "TaskName": "Fetch Cryptocurrency Market Data",
    "TaskDetail": "This script fetches cryptocurrency market data from CoinGecko."
}
*/
```

### 2. Initialize the RestEase Plugin

Get an instance of the **IRestEase** plugin using **PluginLoader**. Then, set the base URL for the CoinGecko API:

```csharp
var restEase = PluginLoader.GetPlugin<ScriptRunner.Plugins.RestEase.IRestEase>();
restEase.SetBaseUrl("https://api.coingecko.com/api/v3");
```

### 3. Define Query Parameters

Specify the parameters required for the CoinGecko API request. These include the target currency, the cryptocurrencies to fetch, and other options:

```csharp
var queryParams = new
{
    vs_currency = "usd",
    ids = "bitcoin,ethereum",
    order = "market_cap_desc",
    per_page = 10,
    page = 1,
    sparkline = false
};
```

### 4. Build the Query String

Convert the query parameters into a properly encoded query string for the API call:

```csharp
var queryString = string.Join("&", queryParams.GetType().GetProperties()
    .Select(p => $"{p.Name}={Uri.EscapeDataString(p.GetValue(queryParams)?.ToString() ?? string.Empty)}"));
```

### 5. Perform the API Request

Use the **GetAsync** method to send the GET request with the query string and receive the response:

```csharp
var response = await restEase.GetAsync($"/coins/markets?{queryString}");
```

### 6. Format and Display the Response

Format the response content as JSON for better readability, then display the result:

```csharp
Dump(response.Content.ReformatJson());
```

### 7. Return the Task Result

Return a success message to indicate the task completion:

```csharp
return "Cryptocurrency data fetched successfully.";
```

---

## Example Script

Here’s the complete script for reference:

```csharp
/*
{
    "TaskCategory": "Networking",
    "TaskName": "Fetch Cryptocurrency Market Data",
    "TaskDetail": "This script fetches cryptocurrency market data from CoinGecko.",
    "RequiredPlugins": ["RestEase"]
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

## Expected Output

When executed, this script will fetch data for Bitcoin and Ethereum, including their market prices, ranks, and other information. The response will be displayed in a neatly formatted JSON structure.

---

## Tips & Notes

- **CoinGecko API Documentation**: Refer to [CoinGecko's API documentation](https://www.coingecko.com/en/api/documentation) for additional endpoints and parameters.
- **Customization**: Modify the query parameters to fetch data for different cryptocurrencies or customize the sorting and pagination options.
- **Error Handling**: Enhance the script to handle API errors or unexpected responses gracefully.
