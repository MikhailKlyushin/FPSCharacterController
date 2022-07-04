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
        
        Debug.Log("CharacterStorage.cs: ��������� � ����� ID �� ����������!");
        return new CharacterModel();    // ���������� ����� ���������� ������?
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
                Debug.Log("CharacterStorage.cs: ��������� � ����� ID ������� ������!");
            }
            else
            {
                Debug.Log("CharacterStorage.cs: �������� ��������� � �������, ��������� � ����� ID �� ����������!");
            }

        }
    }
}
