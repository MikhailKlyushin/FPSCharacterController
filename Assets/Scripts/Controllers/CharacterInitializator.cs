using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CharacterInitializator : MonoBehaviour
{
    private CharacterService _characterService = new CharacterService();

    void Start()
    {
        _characterService.CreatePlayer();
    }
}
