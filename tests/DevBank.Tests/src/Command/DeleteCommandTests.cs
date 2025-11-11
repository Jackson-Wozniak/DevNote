using DevBank.Command;
using DevBank.Exception;
using DevBank.Model;
using DevBank.Tests.Mocks;

namespace DevBank.Tests.Command;

public class DeleteCommandTests
{
    private readonly TestRepository _repository = new();
    private readonly TestConsole _console = new();
    private readonly DeleteCommand _deleteCommand;
    private const string EntriesDeletedString = "Deleted # entries.";

    public DeleteCommandTests()
    {
        _deleteCommand = DeleteCommand.Create(_repository, _console);
    }

    [Fact]
    public void Execute_EmptyArguments_WritesError()
    {
        //message must be included as second
        _deleteCommand.Execute(["delete"]);
        
        Assert.Equal(ExceptionStrings.InvalidDeleteFormatException, _console.Log.Last());
        Assert.Empty(_repository.FindAll());
        _repository.DeleteAll();
        _console.Log.Clear();
    }
    
    [Fact]
    public void Execute_NonPeriod_WritesError()
    {
        //message must be included as second
        _deleteCommand.Execute(["delete", "-"]);
        
        Assert.Equal(ExceptionStrings.InvalidDeleteFormatException, _console.Log.Last());
        Assert.Empty(_repository.FindAll());
        _repository.DeleteAll();
        _console.Log.Clear();
    }

    [Fact]
    public void Execute_Delete_ClearsAll()
    {
        _repository.DeleteAll();
        _console.Log.Clear();
        
        _deleteCommand.Execute(["delete", "."]);
        Assert.Equal(EntriesDeletedString.Replace("#", "0"), _console.Log.Last());
        Assert.Empty(_repository.FindAll());
        _console.Log.Clear();
        
        _repository.Save(new Entry("entry", ["t"], DateTime.Now));
        
        _deleteCommand.Execute(["delete", "."]);
        Assert.Equal(EntriesDeletedString.Replace("#", "1"), _console.Log.Last());
        Assert.Empty(_repository.FindAll());
        _console.Log.Clear();
        
        _repository.Save(new Entry("entry", ["t"], DateTime.Now));
        _repository.Save(new Entry("entry", ["t"], DateTime.Now));
        _repository.Save(new Entry("entry", ["t"], DateTime.Now));
        
        _deleteCommand.Execute(["delete", "."]);
        Assert.Equal(EntriesDeletedString.Replace("#", "3"), _console.Log.Last());
        Assert.Empty(_repository.FindAll());
        _console.Log.Clear();
    }
}