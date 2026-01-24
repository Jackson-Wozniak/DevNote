using System.CommandLine;
using System.Reflection;
using DevBank.Commands;

namespace DevBank;

class Program
{
    public static void Main(string[] args)
    {
        var rootCommand = new RootCommand("DevBank CLI");
        
        rootCommand.Add(SaveCommand.Create());
        rootCommand.Add(ListCommand.Create());
        rootCommand.Add(FindCommand.Create());

        rootCommand.Parse(args).Invoke();
    }
}
