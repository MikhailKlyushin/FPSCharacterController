using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ViewStorage
{
    private List<CharacterView> _views = new List<CharacterView>();
    public int Count => _views.Count;

    public CharacterView GetChatacterView(string characterID)
    {
        //TODO: FIrstOrDefault (Rider's advice)
        return _views.Where(item => item.CharacterID == characterID).FirstOrDefault();
    }


    public void AddCharacterView(CharacterView view)
    {
        _views.Add(view);
    }

    public void RemoveCharacterView(string characterID)
    {
        foreach (var item in _views)
        {
            if (item.CharacterID == characterID)
            {
                _views.Remove(item);
                Debug.Log("ViewStorage.cs: View with this ID successfully deleted!");
            }
            else
            {
                Debug.Log("ViewStorage.cs: Error deleting a view, a view with such an ID does not exist!");
            }

        }
    }
}
