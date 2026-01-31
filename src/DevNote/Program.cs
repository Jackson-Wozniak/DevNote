using System.CommandLine;
using DevNote.Commands;

namespace DevNote;

class Program
{
    public static void Main(string[] args)
    {
        var rootCommand = new RootCommand("DevNote CLI");
        
        rootCommand.Add(SaveCommand.Create());
        rootCommand.Add(ListCommand.Create());
        rootCommand.Add(FindCommand.Create());
        rootCommand.Add(ClearCommand.Create());

        rootCommand.Parse(args).Invoke();
    }
}
