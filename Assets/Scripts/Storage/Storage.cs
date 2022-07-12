using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Storage<T> : IStorage<T> where T : IIdentified
{
    private List<T> _list = new List<T>();
    public int Count => _list.Count;
    
    public T Get(string id)
    {
        return _list.FirstOrDefault(item => item.ID == id);
    }
    
    public void Add(T item)
    {
        _list.Add(item);
    }
    
    public void Remove(string id)
    {
        foreach (var item in _list)
        {
            if (item.ID == id)
            {
                _list.Remove(item);
                Debug.Log("Item with this ID successfully deleted!");
            }
            else
            {
                Debug.Log("Error deleting a item, a item with such an ID does not exist!");
            }

        }
    }
}
