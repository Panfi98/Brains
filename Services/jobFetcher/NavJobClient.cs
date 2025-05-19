using System.Net.Http.Headers;
using System.Text.Json;

namespace BrainsToDo.Services.jobFetcher;

public class NavJobClient
{
    private readonly HttpClient _httpClient;

    // Either inject this value from configuration/secret store
    // or keep a constant as shown below (placeholder used here).
    private readonly string _staticToken = " eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJuYXYudGVhbS5hcmJlaWRzcGxhc3NlbkBuYXYubm8iLCJraWQiOiI5YTY2OTc2MS1hMmFhLTQ2YjQtOWZkNi0yYTQ5YmNjZjJmNjUiLCJpc3MiOiJuYXYtbm8iLCJhdWQiOiJmZWVkLWFwaS12MiIsImlhdCI6MTc0NjcyMTcyOSwiZXhwIjoxNzQ5NzQ1NzI5fQ.wJtAyYwVF2vk1gfdYfrhsBNsew68a0WxGfYVmJHRNYM";

    public NavJobClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task<T?> GetAsync<T>(string url, string? bearerToken = null)
    {
        try
        {
            // Fallback to the static token when caller did not supply one.
            var tokenToUse = string.IsNullOrWhiteSpace(bearerToken)
                ? _staticToken
                : bearerToken;

            using var request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrWhiteSpace(tokenToUse))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", tokenToUse);
            }

            using var response = await _httpClient.SendAsync(
                request,
                HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();

            await using var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(stream);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Request error: {ex.Message}");
            return default;
        }
    }
}