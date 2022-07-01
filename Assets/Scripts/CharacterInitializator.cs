using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CharacterInitializator : MonoBehaviour
{
    private InputProvider _inputProvider;
    private CharacterModel _characterModel;

    void Start()
    {
        _inputProvider = new InputProvider();
        _characterModel = new CharacterModel(_inputProvider);
        _inputProvider.Update();
    }
}
