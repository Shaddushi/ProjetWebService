namespace Entities.SpotifyEntities.UserProfile;
using System.Text.Json.Serialization;
using System.Collections.Generic;

public class UserProfile
{
    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("explicit_content_filter_enabled")]
    public bool ExplicitContentFilterEnabled { get; set; }

    [JsonPropertyName("explicit_content_filter_locked")]
    public bool ExplicitContentFilterLocked { get; set; }

    [JsonPropertyName("external_urls_spotify")]
    public string ExternalUrlsSpotify { get; set; }

    [JsonPropertyName("followers_total")]
    public int FollowersTotal { get; set; }

    [JsonPropertyName("href")]
    public string Href { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("images")]
    public List<Image> Images { get; set; }

    [JsonPropertyName("product")]
    public string Product { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("uri")]
    public string Uri { get; set; }

    public class Image
    {
        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }
    }
}