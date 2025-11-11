using DevBank.Command;
using DevBank.Tests.Mocks;

namespace DevBank.Tests.Command;

public class FindCommandTests
{
    private readonly TestRepository _repository = new();
    private readonly TestConsole _console = new();
    private readonly FindCommand _findCommand;
    private const string EntriesFoundCommand = "Found # entries:";

    public FindCommandTests()
    {
        _findCommand = FindCommand.Create(_repository, _console);
    }

    [Fact]
    public void Execute_EmptyRepository_WritesZeroEntries()
    {
        _repository.DeleteAll();
        _console.Log.Clear();
        
        _findCommand.Execute(["find", "Message"]);

        //last written value is line break, second to last is record count
        var writtenString = _console.Log[^2];
        Assert.Equal(EntriesFoundCommand.Replace("#", "0"), writtenString);
    }
}