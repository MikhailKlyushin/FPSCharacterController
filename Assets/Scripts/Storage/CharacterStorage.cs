using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterStorage
{
    private List<CharacterModel> _characters = new List<CharacterModel>();

    public int Count => _characters.Count;


    public CharacterModel GetChatacterModel(string characterID)
    {
        return _characters.Where(item => item.CharacterID == characterID).FirstOrDefault();
    }


    public void AddCharacterModel(CharacterModel model)
    {
        _characters.Add(model);
    }

    public void RemoveCharacterModel(string characterID)
    {
        foreach (var item in _characters)
        {
            if (item.CharacterID == characterID)
            {
                _characters.Remove(item);
                Debug.Log("CharacterStorage.cs: Character with this ID successfully deleted!");
            }
            else
            {
                Debug.Log("CharacterStorage.cs: Error deleting a character, a character with such an ID does not exist!");
            }

        }
    }
}