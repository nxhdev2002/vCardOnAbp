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

    public static async Task<T> SendRequestAsync<T>(HttpMethod method, string url, Dictionary<string, string> headers = null, Dictionary<string, string> queryParams = null, object body = null)
    {
        try
        {
            // Xây dựng URL với query params nếu có
            if (queryParams != null && queryParams.Count > 0)
            {
                url += "?" + string.Join("&", queryParams.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));
            }

            // Tạo request
            HttpRequestMessage request = new(method, url);

            // Thêm headers nếu có
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            // Thêm body nếu là POST, PUT, PATCH và có body
            if (body != null && (method == HttpMethod.Post || method == HttpMethod.Put || method == HttpMethod.Patch))
            {
                string jsonBody = JsonSerializer.Serialize(body);
                request.Content = new StringContent(jsonBody, System.Text.Encoding.UTF8, "application/json");
            }

            // Gửi request
            HttpResponseMessage response = await _httpClient.SendAsync(request);

            // Đảm bảo thành công, nếu không thì throw exception
            response.EnsureSuccessStatusCode();

            // Đọc và deserialize kết quả
            string responseData = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseData)!;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error during HTTP request: {ex.Message}", ex);
        }
    }
}