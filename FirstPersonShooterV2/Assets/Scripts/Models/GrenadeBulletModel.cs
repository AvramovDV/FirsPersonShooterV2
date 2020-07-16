using UnityEngine;


public class GrenadeBulletModel : MonoBehaviour
{
    #region Fields

    [SerializeField] private Transform _transform;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private float _detenationTime;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    #endregion


    #region Propeties

    public Rigidbody RigidBody { get => _rigidbody; }

    public float Damage { get; set; }

    #endregion


    #region UnityMethods

    private void Start()
    {
        new TimeController(Explosion, _detenationTime, false);
    }

    #endregion


    #region Methods

    private void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(_rigidbody.position, _explosionRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].GetComponent<Rigidbody>())
            {
                colliders[i].GetComponent<Rigidbody>().AddForce(colliders[i].transform.position - _rigidbody.position * _explosionForce);
            }
            ISetDamege target = colliders[i].GetComponent<ISetDamege>();
            if (target != null)
            {
                target.SetDamage(Damage);
            }
        }
        Instantiate(_explosion, _rigidbody.position, _transform.rotation);
        Destroy(gameObject);
    }

    #endregion
}
