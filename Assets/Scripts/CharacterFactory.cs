using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    CharacterModel CharacterModel { get;}

}

public class Player : ICharacter
{
    public CharacterModel CharacterModel => _characterModel;
    private CharacterModel _characterModel;

    public Player(PlayerFactory factory)
    {
        _characterModel = new CharacterModel(factory.CreateMovement());
    }
}

public interface ICharacterFactory
{
    public InputProvider CreateMovement();
}

public class PlayerFactory : ICharacterFactory
{
    public InputProvider CreateMovement()
    {
        return new InputProvider();
    }
}
