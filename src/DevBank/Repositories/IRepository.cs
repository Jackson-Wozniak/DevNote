using DevBank.Models;

namespace DevBank.Repositories;

public interface IRepository
{
    int DeleteAll();
    void Save(Entry entry);
    List<Entry> FindAll(int count = -1);
}