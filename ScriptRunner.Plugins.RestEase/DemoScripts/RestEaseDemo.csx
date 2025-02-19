/*
{
    "TaskCategory": "Plugins",
    "TaskName": "RestEase Demo Script",
    "TaskDetail": "This script fetches cryptocurrency market data from CoinGecko.",
    "RequiredPlugins": ["RestEase"]
}
*/

var restEase = new RestEase();
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
