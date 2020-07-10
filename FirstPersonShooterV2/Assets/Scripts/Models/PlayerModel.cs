using System;
using UnityEngine;


public class PlayerModel : MonoBehaviour
{
    #region Fields

    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _transform;
    [SerializeField] private float _speed;
    [SerializeField] private float _mass;
    [SerializeField] private float _jump;
    [SerializeField] private float _rotationSpeed;

    private float _minimumX = -90f;
    private float _maximumX = 90f;

    #endregion


    #region Propeties

    public Vector3 Forward { get => _transform.forward; }
    public Vector3 Right { get => _transform.right; }
    public float Speed { get => _speed; }
    public float Mass { get => _mass; }
    public float Jump { get => _jump; }
    public float RotationSpeed { get => _rotationSpeed; }
    public bool IsGrounded { get => _characterController.isGrounded; }


    #endregion


    #region Methods

    public void Move(Vector3 movement)
    {
        _characterController.Move(movement);
    }

    public void Rotate(Vector3 rotation)
    {
        _transform.localRotation *= Quaternion.Euler(0f, rotation.y, 0f);
        _cameraTransform.localRotation *= Quaternion.Euler(-rotation.x, 0f, 0f);
        _cameraTransform.localRotation = ClampRotationAroundXAxis(_cameraTransform.localRotation, _minimumX, _maximumX);
    }

    private Quaternion ClampRotationAroundXAxis(Quaternion q, float minimumX, float maximumX)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, minimumX, maximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }

    #endregion
}
