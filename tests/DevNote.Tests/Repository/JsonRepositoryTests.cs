using DevNote.Helpers;
using DevNote.Repositories;

namespace DevNote.Tests.Repository;

public class JsonRepositoryTests
{
    private readonly string _filePath;
    private readonly JsonRepository _repository = new JsonRepository();
    
    public JsonRepositoryTests()
    { 
        var tempDir = Path.Combine(Path.GetTempPath(), "DevNoteTests");
        Directory.CreateDirectory(tempDir);
        _filePath = Path.Combine(tempDir, "entries.json");
        
        if (File.Exists(_filePath)) File.Delete(_filePath);
        File.WriteAllText(_filePath, "");

        JsonRepositoryHelper.OverrideBasePath = _filePath;
    }
}