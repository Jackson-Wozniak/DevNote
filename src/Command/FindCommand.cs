using DevBank.Console;
using DevBank.Repository;

namespace DevBank.Command;

public class FindCommand : ICommand
{
    private readonly IRepository _repository;
    private readonly IConsole _console;

    protected FindCommand(IRepository repository, IConsole console)
    {
        _repository = repository;
        _console = console;
    }

    public static FindCommand Create()
    {
        return new FindCommand(new JsonRepository(), new SystemConsole());
    }

    public static FindCommand Create(IRepository r, IConsole c)
    {
        return new FindCommand(r, c);
    }

    public void Execute(string[] args)
    {
        var phrases = args.Skip(1).ToList();

        var entries = _repository.FindByMessagePhrase(phrases, true);
        
        _console.WriteLine($"Found {entries.Count} entries:");
        _console.WriteLine("");
        foreach (var entry in entries)
        {
            _console.WriteLine($"\"{entry.Message}\"");
            _console.WriteLine($"    tags: [{string.Join(", ", entry.Tags)}]");
            _console.WriteLine($"    created on: {entry.CreatedAt:f}");
            _console.WriteLine("");
        }
    }
}