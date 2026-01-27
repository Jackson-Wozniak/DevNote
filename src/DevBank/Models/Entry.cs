namespace DevBank.Models;

public class Entry
{
    public string Content { get; set; }
    public List<string> Tags { get; set; } = [];
    public DateTime CreatedAt { get; set; }

    public Entry(string content, List<string> tags, DateTime createdAt)
    {
        Tags.AddRange(tags);
        Content = content;
        CreatedAt = createdAt;
    }
}