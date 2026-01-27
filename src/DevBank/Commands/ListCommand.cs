using System.CommandLine;
using DevBank.Consoles;
using DevBank.Repositories;
using DevBank.Models;

namespace DevBank.Commands;

public class ListCommand
{
    private readonly IRepository _repository;
    private readonly IConsole _console;

    protected ListCommand(IRepository repository, IConsole console)
    {
        _repository = repository;
        _console = console;
    }

    public static Command Create(IRepository? r = null, IConsole? c = null)
    {
        return new ListCommand(r ?? new JsonRepository(), c ?? new SystemConsole())
            .CreateCommand();
    }

    private Command CreateCommand()
    {
        var command = new Command("list", "Print entries ordered by date");

        var countOption = new Option<int?>("--count", "-c");
        command.Options.Add(countOption);
        
        command.SetAction(result =>
        {
            int count = result.GetValue(countOption) ?? 25;
            
            Execute(count);
        });

        return command;
    }

    private void Execute(int maxEntries)
    {
        List<Entry> entries = _repository.FindAll()
            .OrderByDescending(e => e.CreatedAt)
            .Take(maxEntries)
            .ToList();
        
        _console.WriteLine($"Found ({entries.Count}) most recent entries:");
        _console.WriteLine("");
        foreach (var entry in entries)
        {
            _console.WriteLine($"\"{entry.Content}\"");
            _console.WriteLine($"    tags: [{string.Join(", ", entry.Tags)}]");
            _console.WriteLine($"    created on: {entry.CreatedAt:f}");
            _console.WriteLine("");
        }
    }
}