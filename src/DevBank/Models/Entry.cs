namespace DevBank.Models;

public class Entry
{
    public string Content { get; set; }
    public string? Project { get; set; }
    public string? Source { get; set; }
    public string? Priority { get; set; }
    public string? Note { get; set; }
    public string? Language { get; set; }
    public string? Link { get; set; }
    public bool IsStarred { get; set; } = false;
    public List<string> Tags { get; set; } = [];
    public DateTime CreatedAt { get; set; }

    public Entry(string content, List<string> tags, DateTime createdAt)
    {
        Tags.AddRange(tags);
        Content = content;
        CreatedAt = createdAt;
    }
}