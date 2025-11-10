using System.Text.Json;
using DevBank.Model;
using DevBank.Repository;

namespace DevBank.Tests.Repository;

public class JsonRepositoryTests
{
    private const string EntriesFilePath = "./Data/test_entries.json";
    private readonly JsonRepository _repository = new JsonRepository(EntriesFilePath);
    
    [Fact]
    public void Save_AppendOne_UpdatesFile()
    {
        var tags = new List<string> {"testTag"};
        var entry = new Entry(tags, "Test Message", DateTime.Now);
        _repository.Save(entry);

        var entries = _repository.FindAll();

        Assert.NotNull(entries);
        Assert.Single(entries);
        Assert.Single(entries[0].Tags);
        Assert.Equal("testTag", entries[0].Tags[0]);
        Assert.Equal("Test Message", entries[0].Message);
    }
}