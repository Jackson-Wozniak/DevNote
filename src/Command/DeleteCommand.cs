using DevBank.Console;
using DevBank.Exception;
using DevBank.Repository;

namespace DevBank.Command;

public class DeleteCommand : ICommand
{
    private readonly IRepository _repository;
    private readonly IConsole _console;

    protected DeleteCommand(IRepository repository, IConsole console)
    {
        _repository = repository;
        _console = console;
    }

    public static DeleteCommand Create()
    {
        return new DeleteCommand(new JsonRepository(), new SystemConsole());
    }

    public static DeleteCommand Create(IRepository r, IConsole c)
    {
        return new DeleteCommand(r, c);
    }

    public void Execute(string[] args)
    {
        if (args.Length != 2 || args[1].ToLower() != ".")
        {
            _console.WriteLine(ExceptionStrings.InvalidDeleteFormatException);
            return;
        }

        var deletedCount = _repository.DeleteAll();
        
        _console.WriteLine($"Deleted {deletedCount} entries.");
    }
}