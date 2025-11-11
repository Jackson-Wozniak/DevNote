using DevBank.Model;
using DevBank.Repository;

namespace DevBank.Tests.Repository;

public class JsonRepositoryTests
{
    private const string EntriesFilePath = "./Data/test_entries.json";
    private readonly JsonRepository _repository = new(EntriesFilePath);
    
    [Fact]
    public void Save_AppendOne_UpdatesFile()
    {
        _repository.DeleteAll();
        
        var tags = new List<string> {"testTag"};
        var entry = new Entry("Test Message", tags, DateTime.Now);
        _repository.Save(entry);

        var entries = _repository.FindAll();

        Assert.NotNull(entries);
        Assert.Single(entries);
        Assert.Single(entries[0].Tags);
        Assert.Equal("testTag", entries[0].Tags[0]);
        Assert.Equal("Test Message", entries[0].Message);
    }
    
    [Fact]
    public void Save_AppendMultiple_UpdatesFile()
    {
        _repository.DeleteAll();
        
        var tags = new List<string> {"testTag"};
        var entry = new Entry("Test Message", tags, DateTime.Now);
        _repository.Save(entry);

        var entries = _repository.FindAll();

        Assert.NotNull(entries);
        Assert.Single(entries);
        Assert.Single(entries[0].Tags);
        Assert.Equal("testTag", entries[0].Tags[0]);
        Assert.Equal("Test Message", entries[0].Message);

        tags = ["testTag2"];
        entry = new Entry("Test Message 2", tags, DateTime.Now);
        _repository.Save(entry);

        entries = _repository.FindAll();
        
        Assert.NotNull(entries);
        Assert.Equal(2, entries.Count);
        Assert.Single(entries[0].Tags);
        Assert.Equal("testTag", entries[0].Tags[0]);
        Assert.Equal("Test Message", entries[0].Message);

        Assert.Single(entries[1].Tags);
        Assert.Equal("testTag2", entries[1].Tags[0]);
        Assert.Equal("Test Message 2", entries[1].Message);
    }

    [Fact]
    public void DeleteAll_Delete_ClearsFile()
    {
        _repository.DeleteAll();
        
        Assert.True(string.IsNullOrEmpty(File.ReadAllText(EntriesFilePath)));
        
        var tags = new List<string> {"testTag"};
        var entry = new Entry("Test Message", tags, DateTime.Now);
        _repository.Save(entry);
        
        _repository.DeleteAll();
        
        Assert.True(string.IsNullOrEmpty(File.ReadAllText(EntriesFilePath)));
    }

    [Fact]
    public void FindAll_ContainsMultiple_ReturnsList()
    {
        _repository.DeleteAll();
        var entries = new List<Entry>()
        {
            new("Message 0", ["tag0"],  DateTime.Now),
            new("Message 1", ["tag1"],  DateTime.Now),
            new("Message 2", ["tag2"],  DateTime.Now),
            new("Message 3", ["tag3"],  DateTime.Now),
            new("Message 4", ["tag4"],  DateTime.Now),
        };
        entries.ForEach(e => _repository.Save(e));

        var returnedEntries = _repository.FindAll();
        
        Assert.Equal(5, returnedEntries.Count);
        for (var i = 0; i < 5; i++)
        {
            Assert.Single(returnedEntries[i].Tags);
            Assert.Equal($"tag{i}", returnedEntries[i].Tags[0]);
            Assert.Equal($"Message {i}", returnedEntries[i].Message);
        }

        returnedEntries = _repository.FindAll(2);
        Assert.Equal(2, returnedEntries.Count);
    }

    [Fact]
    public void FindByTags_EmptyFile_ReturnsNone()
    {
        _repository.DeleteAll();
        Assert.Empty(_repository.FindByTags(["tag", "testTag"]));
    }

    [Fact]
    public void FindByTags_EmptyTagList_ReturnsNone()
    {
        _repository.DeleteAll();
        var entries = new List<Entry>()
        {
            new("Message 0", ["tag0"],  DateTime.Now),
            new("Message 1", ["tag1"],  DateTime.Now),
            new("Message 2", ["tag2"],  DateTime.Now),
            new("Message 3", ["tag3"],  DateTime.Now),
            new("Message 4", ["tag4"],  DateTime.Now),
        };
        entries.ForEach(e => _repository.Save(e));
        
        Assert.Empty(_repository.FindByTags([]));
    }

    [Fact]
    public void FindByTags_MultipleMatches_ReturnsList()
    {
        _repository.DeleteAll();
        var entries = new List<Entry>()
        {
            new("Message 0", ["tag0"],  DateTime.Now),
            new("Message 1", ["tag1"],  DateTime.Now),
            new("Message 2", ["tag2"],  DateTime.Now),
            new("Message 3", ["tag3"],  DateTime.Now),
            new("Message 4", ["tag4"],  DateTime.Now),
        };
        entries.ForEach(e => _repository.Save(e));

        var returnedEntries = _repository.FindByTags(["tag0"]);
        Assert.Single(returnedEntries);
        Assert.Equal("Message 0", returnedEntries[0].Message);
        
        returnedEntries = _repository.FindByTags(["tag0", "tag1", "tag2", "tag3", "tag4"]);
        Assert.Equal(5, returnedEntries.Count);
        Assert.Equal("Message 0", returnedEntries[0].Message);
        Assert.Equal("Message 1", returnedEntries[1].Message);
        Assert.Equal("Message 2", returnedEntries[2].Message);
        Assert.Equal("Message 3", returnedEntries[3].Message);
        Assert.Equal("Message 4", returnedEntries[4].Message);
    }

    [Fact]
    public void FindByMessagePhrase_EmptyPhrase_ReturnsAll()
    {
        _repository.DeleteAll();
        var entries = new List<Entry>()
        {
            new("Message 0", ["tag0"],  DateTime.Now),
            new("Message 1", ["tag1"],  DateTime.Now),
            new("Message 2", ["tag2"],  DateTime.Now),
            new("Message 3", ["tag3"],  DateTime.Now),
            new("Message 4", ["tag4"],  DateTime.Now),
        };
        entries.ForEach(e => _repository.Save(e));

        var returnedEntries = _repository.FindByMessagePhrase("", true);
        Assert.Equal(5, returnedEntries.Count);
    }
    
    [Fact]
    public void FindByMessagePhrase_UnusedPhrase_ReturnsNone()
    {
        _repository.DeleteAll();
        var entries = new List<Entry>()
        {
            new("Message 0", ["tag0"],  DateTime.Now),
            new("Message 1", ["tag1"],  DateTime.Now),
            new("Message 2", ["tag2"],  DateTime.Now),
            new("Message 3", ["tag3"],  DateTime.Now),
            new("Message 4", ["tag4"],  DateTime.Now),
        };
        entries.ForEach(e => _repository.Save(e));

        var returnedEntries = _repository.FindByMessagePhrase("!", true);
        Assert.Empty(returnedEntries);
        
        returnedEntries = _repository.FindByMessagePhrase("!");
        Assert.Empty(returnedEntries);
    }
    
    [Fact]
    public void FindByMessagePhrase_UsedPhrase_ReturnsList()
    {
        _repository.DeleteAll();
        var entries = new List<Entry>()
        {
            new("Message 0", ["tag0"],  DateTime.Now),
            new("Message 1", ["tag1"],  DateTime.Now),
            new("Message 2", ["tag2"],  DateTime.Now),
            new("Message 3", ["tag3"],  DateTime.Now),
            new("Message 4", ["tag4"],  DateTime.Now),
        };
        entries.ForEach(e => _repository.Save(e));

        var returnedEntries = _repository.FindByMessagePhrase("Message");
        Assert.Equal(5, returnedEntries.Count);
        for (var i = 0; i < 5; i++)
        {
            Assert.Equal($"Message {i}", returnedEntries[i].Message);
        }
    }
    
    [Fact]
    public void FindByMessagePhrase_UsedPhrase_IgnoresWhiteSpaces()
    {
        _repository.DeleteAll();
        var entries = new List<Entry>()
        {
            new("Message 0", ["tag0"],  DateTime.Now),
            new("Message 1", ["tag1"],  DateTime.Now),
            new("Message 2", ["tag2"],  DateTime.Now),
            new("Message 3", ["tag3"],  DateTime.Now),
            new("Message 4", ["tag4"],  DateTime.Now),
        };
        entries.ForEach(e => _repository.Save(e));

        var returnedEntries = _repository.FindByMessagePhrase(" Message  ", true);
        Assert.Equal(5, returnedEntries.Count);
        for (var i = 0; i < 5; i++)
        {
            Assert.Equal($"Message {i}", returnedEntries[i].Message);
        }
    }
}