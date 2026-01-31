namespace DevNote.Helpers;

public static class JsonRepositoryHelper
{
    public static string? OverrideBasePath { get; set; }
    
    public static string GetDataPath()
    {
        if (OverrideBasePath is not null) return OverrideBasePath;
        
        string path;
#if DEBUG
        string projectRoot = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
        path = Path.Combine(projectRoot, "TempData");
#else
        path = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "DevNote",
            "Data");
#endif
        Directory.CreateDirectory(path);
        string filePath = Path.Combine(path, "entries.json");
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "");
        }
        return filePath;
    }
}