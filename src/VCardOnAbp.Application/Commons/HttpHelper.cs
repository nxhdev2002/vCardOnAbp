using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace VCardOnAbp.Commons;

public static class HttpHelper
{
    private static readonly HttpClient _httpClient = new();

    public static async Task<T> SendRequestAsync<T>(
        HttpMethod method,
        string url,
        Dictionary<string, string> headers = null,
        Dictionary<string, string> queryParams = null,
        Dictionary<string, string> formData = null,
        object body = null)
    {
        try
        {
            // Ensure the URL starts with http:// or https://
            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
            {
                url = "http://" + url; // Add default scheme if missing
            }

            // Build URL with query params if provided
            if (queryParams != null && queryParams.Count > 0)
            {
                url += "?" + string.Join("&", queryParams.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));
            }

            // Create request
            HttpRequestMessage request = new(method, url);

            // Add headers if provided
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            // Add form data if the method is POST and formData is provided
            var formContent = new MultipartFormDataContent();
            if (method == HttpMethod.Post && formData != null && formData.Count > 0)
            {
                foreach (var kvp in formData)
                {
                    formContent.Add(new StringContent(kvp.Value), kvp.Key);
                }
                request.Content = formContent;
            }
            // If there is a body and method supports it
            else
            {
                string jsonBody = JsonSerializer.Serialize(body);
                request.Content = new StringContent(jsonBody, System.Text.Encoding.UTF8, "application/json");
            }

            // Send the request
            HttpResponseMessage response = await _httpClient.SendAsync(request);

            // Ensure success, otherwise throw exception
            response.EnsureSuccessStatusCode();

            // Read and deserialize the response
            string responseData = await response.Content.ReadAsStringAsync();
            formContent.Dispose();
            return JsonSerializer.Deserialize<T>(responseData)!;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error during HTTP request: {ex.Message}", ex);
        }
    }
}
