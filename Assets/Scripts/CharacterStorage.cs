using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStorage
{
    private List<ICharacter> _characters = new List<ICharacter>();

    public int Count => _characters.Count;

    public ICharacter GetChatacter(Guid characterID)
    {
        foreach (var item in _characters)
        {
            if (item.CharacterModel.CharacterID == characterID)
            {
                return item;
            }
        }
        
        Debug.Log("CharacterStorage.cs: Персонажа с таким ID не существует!");
        return new Player(new PlayerFactory());    // переписать
    }

    public void AddCharacter(ICharacter character)
    {
        _characters.Add(character);
    }

    public void RemoveCharacter(Guid characterID)
    {
        foreach (var item in _characters)
        {
            if (item.CharacterModel.CharacterID == characterID)
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
