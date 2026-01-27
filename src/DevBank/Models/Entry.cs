namespace DevBank.Models;

public class Entry
{
    public string Message { get; set; }
    public List<string> Tags { get; set; } = [];
    public DateTime CreatedAt { get; set; }

    public Entry(string message, List<string> tags, DateTime createdAt)
    {
        Tags.AddRange(tags);
        Message = message;
        CreatedAt = createdAt;
    }
}