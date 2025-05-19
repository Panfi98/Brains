namespace BrainsToDo.Services.jobFetcher;

public class NavJobClient
{
    public HttpClient _HttpClient;

    public NavJobClient(HttpClient httpClient)
    {
        _HttpClient = httpClient;
    }

    public async Task<T?> GetAsync<T>(string url)
    {
        try
        {
            var response = await _HttpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
        
            return await response.Content.ReadFromJsonAsync<T>();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Request error: {ex.Message}");
            return default;
        }
    }
}