using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using API.Models;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Collections.Generic;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InstagramController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly string _appId;
    private readonly string _appSecret;
    private readonly string _redirectUri;

    public InstagramController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient();
        _configuration = configuration;
        _appId = _configuration["Instagram:AppId"]!;
        _appSecret = _configuration["Instagram:AppSecret"]!;
        _redirectUri = _configuration["Instagram:RedirectUri"]!;
    }

    [HttpPost("token")]
    public async Task<IActionResult> GetLongLivedToken([FromBody] TokenRequest request)
    {
        try
        {
            var response = await _httpClient.GetAsync(
                $"https://graph.facebook.com/v19.0/oauth/access_token" +
                $"?grant_type=fb_exchange_token" +
                $"&client_id={_appId}" +
                $"&client_secret={_appSecret}" +
                $"&fb_exchange_token={request.Token}");

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Failed to exchange token");
            }

            var content = await response.Content.ReadAsStringAsync();
            return Ok(JsonSerializer.Deserialize<TokenResponse>(content));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("token/refresh")]
    public async Task<IActionResult> RefreshLongLivedToken([FromBody] TokenRequest request)
    {
        try
        {
            var response = await _httpClient.GetAsync(
                $"https://graph.instagram.com/refresh_access_token" +
                $"?grant_type=ig_refresh_token" +
                $"&access_token={request.Token}");

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Failed to refresh token");
            }

            var content = await response.Content.ReadAsStringAsync();
            return Ok(JsonSerializer.Deserialize<TokenResponse>(content));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("store-token")]
    public async Task<IActionResult> StoreLongLivedToken([FromBody] TokenRequest request)
    {
        try
        {
            // Get the configuration root to modify user secrets
            var configRoot = (IConfigurationRoot)_configuration;
            
            // Get the user secrets provider
            var userSecretsProvider = configRoot.Providers
                .FirstOrDefault(p => p.GetType().Name == "Microsoft.Extensions.Configuration.UserSecrets.UserSecretsConfigurationProvider");

            if (userSecretsProvider != null)
            {
                // Set the value in user secrets
                await Task.Run(() => 
                    ((IConfigurationProvider)userSecretsProvider).Set("Instagram:LongLivedToken", request.Token)
                );
                
                return Ok(new { message = "Token stored successfully" });
            }

            return StatusCode(500, "User secrets provider not found");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("hashtag/search")]
    public async Task<IActionResult> SearchHashtag([FromQuery] string query)
    {
        try
        {
            var token = _configuration["Instagram:LongLivedToken"];
            var userId = _configuration["Instagram:BusinessAccountId"];
            
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Instagram token not found");
            }

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Instagram Business Account ID not found");
            }

            // First get the hashtag ID
            var hashtagUrl = $"https://graph.facebook.com/v19.0/ig_hashtag_search" +
                $"?user_id={userId}" +
                $"&q={query}" +
                $"&access_token={token}";

            var hashtagSearchResponse = await _httpClient.GetAsync(hashtagUrl);

            if (!hashtagSearchResponse.IsSuccessStatusCode)
            {
                var errorContent = await hashtagSearchResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"Error response: {errorContent}");
                return BadRequest($"Failed to search hashtag: {errorContent}");
            }

            var hashtagContent = await hashtagSearchResponse.Content.ReadAsStringAsync();
            var hashtagData = JsonSerializer.Deserialize<JsonElement>(hashtagContent);
            
            if (!hashtagData.GetProperty("data").EnumerateArray().Any())
            {
                return NotFound("No hashtags found");
            }

            var hashtagId = hashtagData.GetProperty("data")[0].GetProperty("id").GetString();

            // Then get the media count for this hashtag
            var mediaResponse = await _httpClient.GetAsync(
                $"https://graph.facebook.com/v19.0/{hashtagId}" +
                $"?fields=id,name,media_count,media" +
                $"&access_token={token}");

            if (!mediaResponse.IsSuccessStatusCode)
            {
                var mediaErrorContent = await mediaResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"Media error response: {mediaErrorContent}");
                return BadRequest($"Failed to get hashtag media: {mediaErrorContent}");
            }

            var mediaContent = await mediaResponse.Content.ReadAsStringAsync();
            return Ok(JsonSerializer.Deserialize<JsonElement>(mediaContent));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
} 