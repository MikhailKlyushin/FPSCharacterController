using UnityEngine;

public interface INetworkViewThread
{
    public void SetModel(ICharacterModel model);
    public void StartThread(Transform transform, Rigidbody rigidbody, Animator animator, CharacterNetworkParams state);
}
