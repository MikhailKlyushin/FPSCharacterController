using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStorage
{
    private List<CharacterModel> _characters = new List<CharacterModel>();

    public int Count => _characters.Count;


    public CharacterModel GetChatacterModel(Guid characterID)
    {
        try
        {
            foreach (var item in _characters)
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


    public void AddCharacterModel(CharacterModel model)
    {
        _characters.Add(model);
    }

    public void RemoveCharacterModel(Guid characterID)
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
