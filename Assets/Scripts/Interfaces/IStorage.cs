using UnityEditor.SceneManagement;

public interface IStorage<T>
{
    public T Get(string id);
    public void Add(T item);
    public void Remove(string id);
}
