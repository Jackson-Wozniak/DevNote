using DevBank.Command;
using DevBank.Exception;
using DevBank.Tests.Mocks;

namespace DevBank.Tests.Command;

public class SaveCommandTests
{
    private readonly TestRepository _repository = new();
    private readonly TestConsole _console = new();
    private readonly SaveCommand _saveCommand;
    private const string EntrySavedString = "Entry saved successfully.";

    public SaveCommandTests()
    {
        _saveCommand = SaveCommand.Create(_repository, _console);
    }

    [Fact]
    public void Execute_EmptyMessage_WritesError()
    {
        //message must be included as second
        _saveCommand.Execute(["save"]);
        
        Assert.Equal(ExceptionStrings.InvalidSaveFormatException, _console.Log.Last());
        Assert.Empty(_repository.FindAll());
        _repository.DeleteAll();
        _console.Log.Clear();
    }

    [Fact]
    public void Execute_TagsInPlaceOfMessage_WritesError()
    {
        //second argument must not be --tags or -t
        _saveCommand.Execute(["save", "--tags", "Message"]);
        
        Assert.Equal(ExceptionStrings.InvalidSaveFormatException, _console.Log.Last());
        Assert.Empty(_repository.FindAll());
        
        //second argument must not be --tags or -t
        _saveCommand.Execute(["save", "-t", "Message"]);
        
        Assert.Equal(ExceptionStrings.InvalidSaveFormatException, _console.Log.Last());
        Assert.Empty(_repository.FindAll());
        _repository.DeleteAll();
        _console.Log.Clear();
    }

    [Fact]
    public void Execute_OutOfOrderTags_WritesError()
    {
        //second argument must be --tags or -t if it exists
        _saveCommand.Execute(["save", "Message", "Message"]);
        
        Assert.Equal(ExceptionStrings.InvalidSaveFormatException, _console.Log.Last());
        Assert.Empty(_repository.FindAll());
        _repository.DeleteAll();
        _console.Log.Clear();
    }

    [Fact]
    public void Execute_MultipleTagFormatAccepted_WritesSuccessfully()
    {
        _saveCommand.Execute(["save", "message", "--tags", "tag", "tag"]);
        Assert.Equal(EntrySavedString, _console.Log.Last());
        _repository.DeleteAll();
        _console.Log.Clear();
        
        _saveCommand.Execute(["save", "message", "-t", "tag", "tag"]);
        Assert.Equal(EntrySavedString, _console.Log.Last());
        _repository.DeleteAll();
    }

    [Fact]
    public void Execute_EmptyTagList_WritesSuccessfully()
    {
        _saveCommand.Execute(["save", "message", "--tags", "tag", "tag"]);
        Assert.Equal(EntrySavedString, _console.Log.Last());
        _repository.DeleteAll();
        _console.Log.Clear();
    }

    [Fact]
    public void Execute_SingleSave_AppendsToRepository()
    {
        _repository.DeleteAll();
        _console.Log.Clear();
        _saveCommand.Execute(["save", "Test Message", "--tags", "tag1", "tag2"]);

        Assert.Single(_repository.FindAll());
        Assert.Equal(EntrySavedString, _console.Log.Last());
        var entry = _repository.FindAll()[0];
        Assert.Equal(2, entry.Tags.Count);
        Assert.Equal("tag1", entry.Tags[0]);
        Assert.Equal("tag2", entry.Tags[1]);
        Assert.Equal("Test Message", entry.Message);
    }
    
    [Fact]
    public void Execute_MultipleSaves_AppendsToRepository()
    {
        _repository.DeleteAll();
        _console.Log.Clear();
        _saveCommand.Execute(["save", "Test Message 0", "--tags", "tag1", "tag2"]);

        Assert.Single(_repository.FindAll());
        Assert.Equal(EntrySavedString, _console.Log.Last());
        
        _saveCommand.Execute(["save", "Test Message 1", "--tags", "tag1"]);
        Assert.Equal(2, _repository.FindAll().Count);
        Assert.Equal(EntrySavedString, _console.Log.Last());
        
        _saveCommand.Execute(["save", "Test Message 2", "--tags", "tag1", "tag2", "tag3"]);
        Assert.Equal(3, _repository.FindAll().Count);
        Assert.Equal(EntrySavedString, _console.Log.Last());
        
        var entry = _repository.FindAll()[0];
        Assert.Equal(2, entry.Tags.Count);
        Assert.Equal("tag1", entry.Tags[0]);
        Assert.Equal("tag2", entry.Tags[1]);
        Assert.Equal("Test Message 0", entry.Message);
        
        entry = _repository.FindAll()[1];
        Assert.Single(entry.Tags);
        Assert.Equal("tag1", entry.Tags[0]);
        Assert.Equal("Test Message 1", entry.Message);
        
        entry = _repository.FindAll()[2];
        Assert.Equal(3, entry.Tags.Count);
        Assert.Equal("tag1", entry.Tags[0]);
        Assert.Equal("tag2", entry.Tags[1]);
        Assert.Equal("tag3", entry.Tags[2]);
        Assert.Equal("Test Message 2", entry.Message);
    }
}