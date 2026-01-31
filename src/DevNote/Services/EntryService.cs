namespace DevNote.Services;

public class EntryService
{
    public static EntryService Instance { get; } = new EntryService();
    
    private EntryService(){ }
}