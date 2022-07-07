using System;
using System.Collections.Generic;
using UnityEngine;

public class ViewStorage : MonoBehaviour
{
    private List<CharacterView> _views = new List<CharacterView>();

    public int Count => _views.Count;


    public CharacterView GetChatacterModel(Guid characterID)
    {
        try
        {
            foreach (var item in _views)
            {
                if (item.CharacterID == characterID)
                {
                    return item;
                }
            }

            throw new Exception("CharacterStorage.cs: Character with this ID not found!");

        }
        catch (Exception e)
        {
            Debug.Log(e);
            return null;
        }
    }


    public void AddCharacterView(CharacterView view)
    {
        _views.Add(view);
    }

    public void RemoveCharacterView(Guid characterID)
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
