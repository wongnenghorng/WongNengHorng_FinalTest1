using Newtonsoft.Json;

public class PostRecord
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("userId")]
    public int UserId { get; set; } // 修复：将 string 改为 int

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("body")]
    public string Body { get; set; }
}
