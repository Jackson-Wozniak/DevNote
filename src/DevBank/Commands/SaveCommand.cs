using System.CommandLine;
using DevBank.Consoles;
using DevBank.Models;
using DevBank.Repositories;

namespace DevBank.Commands;

public class SaveCommand
{
    private readonly IRepository _repository;
    private readonly IConsole _console;

    private SaveCommand(IRepository repository, IConsole console)
    {
        _repository = repository;
        _console = console;
    }

    public static Command Create(IRepository? r = null, IConsole? c = null)
    {
        return new SaveCommand(r ?? new JsonRepository(), c ?? new SystemConsole())
            .CreateCommand();
    }

    private Command CreateCommand()
    {
        var command = new Command("save", "Save a new entry");
        
        var contentArg = new Argument<string>(name: "content")
        {
            Arity = ArgumentArity.ExactlyOne
        };
        
        command.Arguments.Add(contentArg);
        
        var tagsOption = new Option<string[]>("--tags", "-t")
        {
            AllowMultipleArgumentsPerToken = true
        };
        
        command.Options.Add(tagsOption);
        
        command.SetAction(result =>
        {
            string? content = result.GetValue(contentArg);
            
            if (string.IsNullOrEmpty(content))
            {
                _console.WriteLine("Error: --message is required");
                _console.WriteLine("Usage: DevBank save --message <value> [--tags ...]");
                return;
            }
            
            var tags = result.GetValue(tagsOption) ?? [];
            Execute(content, tags.ToList());
        });

        return command;
    }

    private void Execute(string message, List<string> tags)
    {
        var entry = new Entry(message, tags, DateTime.Now);
        
        _repository.Save(entry);
        _console.WriteLine("Entry saved successfully.");
    }
}