using UnityEngine;


public class BulletModel : MonoBehaviour
{
    #region Fields

    [SerializeField] private Transform _transform;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private float _speed;
    private float _lifeTime = 10f;

    #endregion


    #region Propeties

    public float Damage { get; set; }

    #endregion


    #region UnityMethods

    private void Start()
    {
        _rigidBody.velocity = _transform.forward * _speed;
        Destroy(gameObject, _lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ISetDamege target = collision.gameObject.GetComponent<ISetDamege>();
        if (target != null)
        {
            target.SetDamage(Damage);
        }
        Destroy(gameObject);
    }

    #endregion

}
