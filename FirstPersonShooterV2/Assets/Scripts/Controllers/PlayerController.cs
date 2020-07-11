using UnityEngine;


public class PlayerController : BaseController
{
    #region Fields

    private PlayerModel _model;

    private Vector2 _input;
    private Vector3 _movement;
    private float _gravityForce;

    #endregion


    #region ClassLifeCycles

    public PlayerController()
    {
        _model = Object.FindObjectOfType<PlayerModel>();
        Cursor.lockState = CursorLockMode.Locked;
        On();
    }

    #endregion


    #region Methods

    public override void On()
    {
        base.On();
        ServiceLocatorMonoBehaviour.GetService<GameController>().OnUpdate += PlayerLogic;
    }

    public override void Off()
    {
        base.Off();
        ServiceLocatorMonoBehaviour.GetService<GameController>().OnUpdate -= PlayerLogic;
    }

    private void PlayerLogic()
    {
        Move();
        GamingGravity();
        Rotation();
    }

    private void Move()
    {
        if (_model.IsGrounded)
        {
            _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Vector3 desiredMove = _model.Forward * _input.y + _model.Right * _input.x;

            _movement.x = desiredMove.x * _model.Speed;
            _movement.z = desiredMove.z * _model.Speed;
        }

        _movement.y = _gravityForce;

        _model.Move(_movement * Time.deltaTime);
    }

    private void GamingGravity()
    {
        if (!_model.IsGrounded) _gravityForce -= _model.Mass * Time.deltaTime;
        else _gravityForce = -1;
        if (Input.GetKeyDown(KeyCode.Space) && _model.IsGrounded) _gravityForce = _model.Jump;
    }

    private void Rotation()
    {
        float rotX = Input.GetAxis("Mouse X") * _model.RotationSpeed;
        float rotY = Input.GetAxis("Mouse Y") * _model.RotationSpeed;

        Vector3 rotation = new Vector3(rotY, rotX, 0f);

        _model.Rotate(rotation);
    }

    #endregion
}
