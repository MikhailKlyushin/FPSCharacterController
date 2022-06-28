using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private Animator _animator;

    private float _speedHorizontal;
    private float _speedVertical;



    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        PlayStrafeAnimations();
    }

    void FixedUpdate()
    {
        InputAxis();
        _animator.speed = GetCurrenSpeed(_speedHorizontal, _speedVertical);
    }

    private void InputAxis()
    {
        _speedHorizontal = Input.GetAxis("Horizontal");
        _speedVertical = Input.GetAxis("Vertical");
    }

    private void PlayStrafeAnimations()
    {
        _animator.SetFloat("Horizontal", _speedHorizontal);
        _animator.SetFloat("Vertical", _speedVertical);
    }

    private float GetCurrenSpeed(float speedHorizontal, float speedVertical)
    {
        speedHorizontal = Mathf.Abs(speedHorizontal);
        speedVertical = Mathf.Abs(speedVertical);

        if (speedHorizontal >= speedVertical)
        {
            return speedHorizontal;
        }
        else
        {
            return speedVertical;
        }
    }
}
