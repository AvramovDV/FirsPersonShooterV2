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

        GameObject holePrefub = Resources.Load<GameObject>(StaticData.BulletHolePath);
        GameObject hole = Instantiate(holePrefub, collision.GetContact(0).point, Quaternion.FromToRotation(Vector3.back, collision.GetContact(0).normal));
        hole.transform.position -= hole.transform.forward * 0.4f;

        Destroy(hole, 20f);

        Destroy(gameObject);
    }

    #endregion

}
