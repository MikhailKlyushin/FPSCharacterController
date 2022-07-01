using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterService
{
    private readonly CharacterStorage _storage = new CharacterStorage();

    public ICharacter GetChatacter(Guid characterID)
    {
        return _storage.GetChatacter(characterID);
    }

    public Player CreatePlayer()
    {
        Player player = new Player(new PlayerFactory());
        _storage.AddCharacter(player);
        return player;
    }

}
