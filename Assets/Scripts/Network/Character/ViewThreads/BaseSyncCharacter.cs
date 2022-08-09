using UniRx;
using UnityEngine;

public class BaseSyncCharacter
{
    private readonly int _horizontal = Animator.StringToHash("Horizontal");
    private readonly int _vertical = Animator.StringToHash("Vertical");
    
    protected readonly CompositeDisposable _disposables = new CompositeDisposable();
    
    protected void SetSyncCharacterMove(Transform transform, Rigidbody rigidbody,Vector3 velocity, Quaternion rotate)
    {
        rigidbody.velocity = transform.TransformDirection(velocity);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotate, 0.4f);
    }
    
    protected void SetSyncCharacterPosition(Transform transform, Vector3 position)
    {
        transform.position = Vector3.Lerp(transform.position, position, 0.4f);
    }

    protected void SetSyncAnimatorParams(Animator animator, float directionHorizontal, float directionVertical, float speed)
    {
        animator.SetFloat(_horizontal, directionHorizontal);
        animator.SetFloat(_vertical, directionVertical);
        animator.speed = speed;
    }
}
