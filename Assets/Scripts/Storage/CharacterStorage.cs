using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStorage
{
    private List<CharacterModel> _characters = new List<CharacterModel>();

    public int Count => _characters.Count;


    public CharacterModel GetChatacterModel(Guid characterID)
    {
        foreach (var item in _characters)
        {
            if (item.CharacterID == characterID)
            {
                return item;
            }
        }
        
        Debug.Log("CharacterStorage.cs: Персонажа с таким ID не существует!");
        return new CharacterModel();    // переписать через обработчик ошибок?
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
                Debug.Log("CharacterStorage.cs: Персонажа с таким ID успешно удален!");
            }
            else
            {
                Debug.Log("CharacterStorage.cs: Удаление произошло с ошибкой, персонажа с таким ID не существует!");
            }

        }
    }
}
