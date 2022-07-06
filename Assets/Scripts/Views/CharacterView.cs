using UnityEngine;


public class CharacterView : MonoBehaviour
{
    private CharacterModel _model;
    private Rigidbody _rigidbody;
    private Animator _animator;

    private float _directionHorizontal;
    private float _directionVertical;

    //TODO: Initialize()
    public GameObject InicializateView(CharacterModel model, GameObject characterPrefab)
    {
        GameObject instanceObject = Instantiate(characterPrefab, new Vector3(), new Quaternion());
        instanceObject.GetComponent<CharacterView>().SetModel(model);

        return instanceObject;
    }

    public void SetModel(CharacterModel model)
    {
        _model = model;
    }


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

    }

    private void Update()
    {
        RunStrafeAnimations();
    }

    private void FixedUpdate()
    {
        UpdateViewModel();
    }

    private void UpdateViewModel()
    {
        _rigidbody.velocity = transform.TransformDirection(_model.Velocity);
        transform.localEulerAngles = _model.LocalRotateAngleY;

        _directionHorizontal = _model.InputVector.x;
        _directionVertical = _model.InputVector.z;

        _animator.speed = _model.MoveSpeed;

        Debug.DrawRay(transform.position, _model.Velocity, Color.green);
    }

    private void RunStrafeAnimations()
    {
        //TODO: cache this: private static readonly int _horizontal = Animator.StringToHash("Horizontal"); by example
        _animator.SetFloat("Horizontal", _directionHorizontal);
        _animator.SetFloat("Vertical", _directionVertical);
    }
}
