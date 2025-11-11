using DevBank.Model;
using DevBank.Repository;

namespace DevBank.Tests.Mocks;

public class TestRepository : IRepository
{
    private readonly List<Entry> entries = [];

    public void Save(Entry entry)
    {
        entries.Add(entry);
    }

    public List<Entry> FindAll(int count = -1)
    {
        if (count < 0) return entries;
        return entries.Take(count).ToList();
    }

    public List<Entry> FindByTags(List<string> tags)
    {
        return entries.Where(entry => entry.Tags.Any(tags.Contains)).ToList();
    }

    public List<Entry> FindByMessagePhrase(string phrase, bool ignoreWhiteSpace = false)
    {
        if (ignoreWhiteSpace) phrase = phrase.Replace(" ", "");

        return entries.Where(entry =>
        {
            var message = ignoreWhiteSpace ? entry.Message.Replace(" ", "") : entry.Message;
            return message.Contains(phrase);
        }).ToList();
    }
    
    public List<Entry> FindByMessagePhrase(List<string> phrases, bool ignoreWhiteSpace = false)
    {
        throw new NotImplementedException();
    }

    public int DeleteAll()
    {
        var count = entries.Count;
        entries.Clear();
        return count;
    }
}