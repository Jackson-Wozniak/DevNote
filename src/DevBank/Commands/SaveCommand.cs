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
        var projectsOption = new Option<string>("--project", "-p");
        var sourceOption = new Option<string>("--source", "-s");
        var noteOption = new Option<string>("--note", "-n");
        var linkOption = new Option<string>("--link", "-l");
        var langOption = new Option<string>("--lang");
        var starredOption = new Option<bool>("--star");
        
        command.Options.Add(tagsOption);
        command.Options.Add(projectsOption);
        command.Options.Add(sourceOption);
        command.Options.Add(noteOption);
        command.Options.Add(linkOption);
        command.Options.Add(langOption);
        command.Options.Add(starredOption);
        
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
            var project = result.GetValue(projectsOption);
            var source = result.GetValue(sourceOption);
            var note = result.GetValue(noteOption);
            var link = result.GetValue(linkOption);
            var language = result.GetValue(langOption);
            var starred = result.GetValue(starredOption);
            Execute(content, project, source, note, 
                link, language, starred, tags.ToList());
        });

        return command;
    }

    private void Execute(string content, string? project, string? source,
        string? note, string? link, string? language, bool starred, List<string> tags)
    {
        var entry = new Entry(content, tags, DateTime.Now)
        {
            Project = project,
            Source = source,
            Note = note,
            Link = link,
            Language = language,
            IsStarred = starred
        };
        
        _repository.Save(entry);
        _console.WriteLine("Entry saved successfully.");
    }
}