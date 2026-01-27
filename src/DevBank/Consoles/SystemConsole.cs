namespace DevBank.Consoles;

public class SystemConsole : IConsole
{
    public void Write(string str) => System.Console.Write(str);
    public void WriteLine(string str) => System.Console.WriteLine(str);
}