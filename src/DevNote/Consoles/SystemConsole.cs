namespace DevNote.Consoles;

public class SystemConsole : IConsole
{
    private SystemConsole(){ }

    public static SystemConsole Instance { get; } = new SystemConsole();

    public void Write(string str) => Console.Write(str);
    public void WriteLine(string str) => Console.WriteLine(str);
}