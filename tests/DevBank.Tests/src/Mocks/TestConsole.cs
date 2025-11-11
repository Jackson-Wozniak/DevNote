using DevBank.Console;

namespace DevBank.Tests.Mocks;

public class TestConsole : IConsole
{
    public List<string> Log = [];
    
    public void Write(string str)
    {
        Log.Add(str);
    }

    public void WriteLine(string str)
    {
        Log.Add(str);
    }
}