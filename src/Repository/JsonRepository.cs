using System.Text;
using System.Text.Json;
using DevBank.Model;

namespace DevBank.Repository;

public class JsonRepository : IRepository
{
    private readonly string _dataFilePath;
    
    //defaults to file path but allows for injecting a json file for tests
    public JsonRepository(string file = "./Data/entries.json")
    {
        _dataFilePath = file;
    }

    private List<Entry> ReadFromFile()
    {
        var entries = JsonSerializer.Deserialize<List<Entry>>(File.ReadAllText(_dataFilePath));
        return entries ?? [];
    }

    private void WriteToFile(List<Entry> entries)
    {
        var entriesStr = JsonSerializer.Serialize(entries);
        File.WriteAllText(_dataFilePath, entriesStr, Encoding.UTF8);
    }
    
    public void Save(Entry entry)
    {
        var entries = ReadFromFile();
        entries.Add(entry);
        WriteToFile(entries);
    }

    public List<Entry> FindByMessagePhrase(string phrase)
    {
        throw new NotImplementedException();
    }

    public List<Entry> FindByTags(List<string> tags)
    {
        throw new NotImplementedException();
    }

    public List<Entry> FindAll(int? count)
    {
        var entries = ReadFromFile();
        if (count is null or < 0) return entries;
        return entries.Take(count.Value).ToList();
    }
}