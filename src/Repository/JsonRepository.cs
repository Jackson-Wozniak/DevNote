using System.Text;
using System.Text.Json;
using DevBank.Model;

namespace DevBank.Repository;

public class JsonRepository : IRepository
{
    private readonly string _dataFilePath;
    private readonly JsonSerializerOptions _options = new() { WriteIndented = true };
    
    //defaults to file path but allows for injecting a json file for tests
    public JsonRepository(string file = "./Data/entries.json")
    {
        if (!File.Exists(file))
        {
            using(File.Create(file)){ }
        }
        _dataFilePath = file;
    }

    private List<Entry> ReadFromFile()
    {
        var fileContent = File.ReadAllText(_dataFilePath);
        if (string.IsNullOrWhiteSpace(fileContent)) return [];
        var entries = JsonSerializer.Deserialize<List<Entry>>(fileContent);
        return entries ?? [];
    }

    private void WriteToFile(List<Entry> entries)
    {
        var entriesStr = JsonSerializer.Serialize(entries, _options);
        File.WriteAllText(_dataFilePath, entriesStr, Encoding.UTF8);
    }

    public int DeleteAll()
    {
        var count = ReadFromFile().Count;
        File.WriteAllText(_dataFilePath, "");
        return count;
    }
    
    public void Save(Entry entry)
    {
        var entries = ReadFromFile();
        entries.Add(entry);
        WriteToFile(entries);
    }

    public List<Entry> FindByMessagePhrase(string phrase, bool ignoreWhiteSpace = false)
    {
        if (ignoreWhiteSpace) phrase = phrase.Replace(" ", "");

        return ReadFromFile().Where(entry =>
        {
            var message = ignoreWhiteSpace ? entry.Message.Replace(" ", "") : entry.Message;
            return message.Contains(phrase);
        }).ToList();
    }
    
    public List<Entry> FindByMessagePhrase(List<string> phrases, bool ignoreWhiteSpace = false)
    {
        if (ignoreWhiteSpace) phrases = phrases.Select(p => p.Replace(" ", "")).ToList();

        return ReadFromFile().Where(entry =>
        {
            var message = ignoreWhiteSpace ? entry.Message.Replace(" ", "") : entry.Message;
            return phrases.Any(phrase => message.Contains(phrase));
        }).ToList();
    }

    public List<Entry> FindByTags(List<string> tags)
    {
        var entries = ReadFromFile();
        return entries.Where(entry => entry.Tags.Any(tags.Contains)).ToList();
    }

    public List<Entry> FindAll(int count = -1)
    {
        var entries = ReadFromFile();
        if (count < 0) return entries;
        return entries.Take(count).ToList();
    }
}