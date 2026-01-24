using System.Reflection;
using DevBank.Command;
using DevBank.Exception;

namespace DevBank;

class Program
{
    public static void Main(string[] args)
    {
        var command = args[0].ToLower();

        switch (command)
        {
            case "save":
                SaveCommand.Create().Execute(args);
                break;
            case "find":
                FindCommand.Create().Execute(args);
                break;
            case "list":
                ListCommand.Create().Execute(args);
                break;
            case "delete":
                DeleteCommand.Create().Execute(args);
                break;
            case "--version":
            case "-v":
                var version = Assembly
                    .GetExecutingAssembly()
                    .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
                    .InformationalVersion ?? "unknown";
                version = version.Split("+")[0];
                System.Console.WriteLine($"DevBank v{version}");
                break;
            default:
                System.Console.WriteLine(ExceptionStrings.NoArgumentsException);
                break;
        }
    }
}
