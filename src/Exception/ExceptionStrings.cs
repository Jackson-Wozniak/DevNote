namespace DevBank.Exception;

public static class ExceptionStrings
{
    public const string NoArgumentsException = 
        """
        Usage: devbank <command> [options]\n
        Available commands:
            save    add new entry
            list    list entries with matching attributes
            find    list entries with matching message phrase
        """;
    
    public const string InvalidSaveFormatException = 
        """
        Error: Invalid arguments for 'save'.
        Usage: devbank save "<message>" [--tags <tag>...]
        """;
    
    public const string InvalidListFormatException = 
        """
        Error: Invalid arguments for 'list'.
        Usage: devbank list [--tags <tag>...]
        """;
    
    public const string InvalidFindFormatException = 
        """
        Error: Invalid arguments for 'find'.
        Usage: devbank find "<message phrase or substring>"
        """;
    
    public const string InvalidDeleteFormatException = 
        """
        Error: Invalid arguments for 'find'.
        Usage:
            app delete .                   Deletes all entries
            app delete [--id <id>...]      Deletes one or more entries by ID
        """;
}