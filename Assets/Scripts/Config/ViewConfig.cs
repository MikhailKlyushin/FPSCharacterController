using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ViewConfig", menuName = "Configuration Script/View Config", order = 2)]

public class ViewConfig : ScriptableObject
{
    #region [SerializeField]

    [SerializeField] private GameObject _prefab;

    #endregion

    //TODO: It's not using
    public GameObject Prefab => _prefab;
}
