using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
public class Player : ICharacter
{
    public CharacterModel CharacterModel => _characterModel;
    private CharacterModel _characterModel;

    public Player(PlayerFactory factory)
    {
        _characterModel = new CharacterModel(factory.CreateMovement());
    }
}*/


public class PlayerFactory : ICharacterFactory
{
    public CharacterModel CreateCharacter()
    {
        CharacterModel characterModel = new CharacterModel(new InputKeyAndMouse());
        return characterModel;
    }
}
