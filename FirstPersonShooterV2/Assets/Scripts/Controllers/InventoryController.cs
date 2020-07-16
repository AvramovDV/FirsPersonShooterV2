using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class InventoryController : BaseController
{
    #region Fields

    private BaseWeaponModel _choosenWeapon;

    private List<BaseWeaponModel> _weapons = new List<BaseWeaponModel>();

    private Dictionary<BulletType, int> _bullets = new Dictionary<BulletType, int>();

    private Transform _weaponPoint;

    private BulletsUIModel _bulletsUImodel;

    private bool _isReloading = false;

    #endregion


    #region Propeties

    public BaseWeaponModel ChoosenWeapon { get => _choosenWeapon; }
    public Dictionary<BulletType, int> Bullets { get => _bullets; }

    #endregion


    #region ClassLifeCycles

    public InventoryController()
    {
        _weaponPoint = Object.FindObjectOfType<PlayerModel>().WeaponPoint;
        _bulletsUImodel = Object.FindObjectOfType<BulletsUIModel>();
        On();
    }

    #endregion


    #region Methods

    public override void On()
    {
        base.On();
        ServiceLocatorMonoBehaviour.GetService<GameController>().OnUpdate += ShowBulletsCount;
    }

    public void AddWeapon(BaseWeaponModel weapon)
    {
        if (_weapons.Find(w => w.BulletType == weapon.BulletType))
        {
            AddBullets(weapon.BulletType, weapon.Bullets);
            GameObject.Destroy(weapon.gameObject);
        }
        else
        {
            _weapons.Add(weapon);

            weapon.RigidBody.isKinematic = true;
            weapon.transform.position = _weaponPoint.position;
            weapon.transform.rotation = _weaponPoint.rotation;
            weapon.transform.SetParent(_weaponPoint);

            if (_choosenWeapon == null)
            {
                _choosenWeapon = weapon;
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            AddBullets(weapon.BulletType, 0);
        }
    }

    public void AddBullets(BulletType type, int count)
    {
        if (_bullets.ContainsKey(type))
        {
            _bullets[type] += count;
        }
        else
        {
            _bullets.Add(type, count);
        }
    }

    public void ShowBulletsCount()
    {
        if (_choosenWeapon == null)
        {
            _bulletsUImodel.SetBulletsCount("");
            return;
        }

        _bulletsUImodel.SetBulletsCount($"{_choosenWeapon.BulletType}:\n {_choosenWeapon.Bullets} / {_bullets[_choosenWeapon.BulletType]}");
    }

    public void DropDownWeapon()
    {
        if (_choosenWeapon != null)
        {
            _choosenWeapon.RigidBody.isKinematic = false;
            _choosenWeapon.transform.parent = null;
            _weapons.Remove(_choosenWeapon);
            if (_weapons.Count > 0)
            {
                _choosenWeapon = _weapons[0];
                _choosenWeapon.gameObject.SetActive(true);
            }
            else
            {
                _choosenWeapon = null;
            }
        }
    }

    public void ChooseWeapon(int number, bool concrete = true)
    {
        if (_choosenWeapon == null)
        {
            return;
        }
        if (concrete)
        {
            if (number < _weapons.Count)
            {
                _choosenWeapon.gameObject.SetActive(false);
                _choosenWeapon = _weapons.ToList()[number];
                _choosenWeapon.gameObject.SetActive(true);
            }
        }
        else
        {
            int index = _weapons.IndexOf(_choosenWeapon) + number;
            if (index >= _weapons.Count)
            {
                index = 0;
            }
            if (index < 0)
            {
                index = _weapons.Count - 1;
            }

            _choosenWeapon.gameObject.SetActive(false);
            _choosenWeapon = _weapons[index];
            _choosenWeapon.gameObject.SetActive(true);

        }
    }

    #endregion
}
