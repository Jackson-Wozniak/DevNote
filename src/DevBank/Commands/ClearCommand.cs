using System.CommandLine;
using DevBank.Console;
using DevBank.Repository;

namespace DevBank.Commands;

public class ClearCommand
{
    private readonly IRepository _repository;
    private readonly IConsole _console;

    protected ClearCommand(IRepository repository, IConsole console)
    {
        _repository = repository;
        _console = console;
    }

    public static Command Create(IRepository? r = null, IConsole? c = null)
    {
        return new ClearCommand(r ?? new JsonRepository(), c ?? new SystemConsole())
            .CreateCommand();
    }

    private Command CreateCommand()
    {
        var command = new Command("clear", "Clears all entries");
        
        command.SetAction(_ => Execute());

        return command;
    }

    private void Execute()
    {
        var count = _repository.DeleteAll();
        
        _console.WriteLine($"Cleared ({count}) entries.");
        _console.WriteLine("");
    }
}