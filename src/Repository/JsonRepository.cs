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
        if (!File.Exists(file))
        {
            using(var stream = File.Create(file)){ }
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
        var entriesStr = JsonSerializer.Serialize(entries);
        File.WriteAllText(_dataFilePath, entriesStr, Encoding.UTF8);
    }
    
    public void Save(Entry entry)
    {
        var entries = ReadFromFile();
        entries.Add(entry);
        WriteToFile(entries);
    }

    public List<Entry> FindByMessagePhrase(string phrase, bool ignoreWhiteSpace = false)
    {
        throw new NotImplementedException();
    }

    public List<Entry> FindByTags(List<string> tags)
    {
        throw new NotImplementedException();
    }

    public List<Entry> FindAll(int count = -1)
    {
        var entries = ReadFromFile();
        if (count < 0) return entries;
        return entries.Take(count).ToList();
    }
}