using DevBank.Model;

namespace DevBank.Repository;

public interface IRepository
{
    int DeleteAll();
    void Save(Entry entry);
    List<Entry> FindByMessagePhrase(string phrase, bool ignoreWhiteSpace = false);
    List<Entry> FindByMessagePhrase(List<string> phrases, bool ignoreWhiteSpace = false);
    List<Entry> FindByTags(List<string> tags);
    List<Entry> FindAll(int count = -1);
}