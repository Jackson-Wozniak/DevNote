using System.Text.Json;
using DevBank.Console;
using DevBank.Exception;
using DevBank.Model;
using DevBank.Repository;

namespace DevBank.Command;

public class SaveCommand : ICommand
{
    private readonly IRepository _repository;
    private readonly IConsole _console;

    protected SaveCommand(IRepository repository, IConsole console)
    {
        _repository = repository;
        _console = console;
    }

    public static SaveCommand Create()
    {
        return new SaveCommand(new JsonRepository(), new SystemConsole());
    }

    public static SaveCommand Create(IRepository r, IConsole c)
    {
        return new SaveCommand(r, c);
    }

    public void Execute(string[] args)
    {
        if (args.Length < 2 || args[1].ToLower() == "-t" || args[1].ToLower() == "--tags")
        {
            _console.WriteLine(ExceptionStrings.InvalidSaveFormatException);
            return;
        }
        
        var message = args[1];
        
        if (args.Length > 2 && args[2].ToLower() != "-t" && args[2].ToLower() != "--tags")
        {
            _console.WriteLine(ExceptionStrings.InvalidSaveFormatException);
            return;
        }

        var tags = args.Skip(3).ToList();

        var entry = new Entry(message, tags, DateTime.Now);
        
        _repository.Save(entry);
        _console.WriteLine("Entry saved successfully.");
    }
}