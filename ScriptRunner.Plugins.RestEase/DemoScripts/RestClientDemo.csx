/*
{
    "TaskCategory": "Networking",
    "TaskName": "Fetch Cryptocurrency Market Data",
    "TaskDetail": "This script fetches cryptocurrency market data from CoinGecko."
}
*/

// Define query parameters
var queryParams = new
{
    vs_currency = "usd", // The currency to convert prices to (e.g., usd, eur).
    ids = "bitcoin,ethereum", // Comma-separated IDs of the cryptocurrencies to fetch.
    order = "market_cap_desc", // Order results by market cap.
    per_page = 10, // Number of results per page.
    page = 1, // Page number.
    sparkline = false // Include sparkline data (small price chart) or not.
};

// Create and configure the RestService
var restService = new RestService();
restService.SetBaseUrl("https://api.coingecko.com/api/v3");

// Convert query parameters to query string
var queryString = string.Join("&", queryParams.GetType().GetProperties()
    .Select(p => $"{p.Name}={Uri.EscapeDataString(p.GetValue(queryParams)?.ToString() ?? string.Empty)}"));

// Perform a GET request with the query string
var response = await restService.GetAsync($"/coins/markets?{queryString}");

Dump(response.Content.ReformatJson());

return "Restful test completed";