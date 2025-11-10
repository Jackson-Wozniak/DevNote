using DevBank.Model;

namespace DevBank.Repository;

public interface IRepository
{
    void Save(Entry entry);
    List<Entry> FindByMessagePhrase(string phrase);
    List<Entry> FindByTags(List<string> tags);
    List<Entry> FindAll(int? count);
}