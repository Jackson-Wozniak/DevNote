using System.CommandLine;
using DevBank.Consoles;
using DevBank.Repositories;

namespace DevBank.Commands;

public class FindCommand
{
    private readonly IRepository _repository;
    private readonly IConsole _console;

    protected FindCommand(IRepository repository, IConsole console)
    {
        _repository = repository;
        _console = console;
    }

    public static Command Create(IRepository? r = null, IConsole? c = null)
    {
        return new FindCommand(r ?? new JsonRepository(), c ?? new SystemConsole())
            .CreateCommand();
    }

    private Command CreateCommand()
    {
        var command = new Command("find", "Find matching entries");

        var messageOption = new Option<string>("--message", "-m");
        var tagsOption = new Option<string[]>("--tags", "-t")
        {
            AllowMultipleArgumentsPerToken = true
        };
        
        command.Options.Add(messageOption);
        command.Options.Add(tagsOption);
        
        command.SetAction(result =>
        {
            string? message = result.GetValue(messageOption);
            
            if (string.IsNullOrEmpty(message))
            {
                _console.WriteLine("Error: --message is required");
                _console.WriteLine("Usage: DevBank save --message <value> [--tags ...]");
                return;
            }
            
            var tags = result.GetValue(tagsOption) ?? [];
            Execute(message, tags.ToList());
        });

        return command;
    }

    private void Execute(string phrase, List<string> tags)
    {
        var entries = _repository.FindByMessagePhrase(phrase, true)
            .Where(_ => tags.Count == 0 || tags.Any(tags.Contains))
            .OrderByDescending(e => e.CreatedAt)
            .ToList();
        
        _console.WriteLine($"Found ({entries.Count}) matching entries:");
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