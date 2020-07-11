using UnityEngine;


public abstract class BaseWeaponModel : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject _bulletPrefub;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Rigidbody _rigidBody;

    [SerializeField] private BulletType _bulletType;
    [SerializeField] private float _fireSpeed;
    [SerializeField] private float _reloadSpeed;
    [SerializeField] private float _damage;
    [SerializeField] private int _maxBullets;
    [SerializeField] private int _bullets;

    #endregion


    #region Propeties

    public Rigidbody RigidBody { get => _rigidBody; }
    public GameObject BulletPrefub { get => _bulletPrefub; }
    public Transform FirePoint { get => _firePoint; }
    public BulletType BulletType { get => _bulletType; }
    public float FireSpeed { get => _fireSpeed; }
    public float ReloadSpeed { get => _reloadSpeed; }
    public float Damage { get => _damage; }
    public int MaxBullets { get => _maxBullets; }
    public int Bullets { get => _bullets; set => _bullets = value; }

    #endregion


    #region Methods

    public abstract void Fire();

    #endregion
}
