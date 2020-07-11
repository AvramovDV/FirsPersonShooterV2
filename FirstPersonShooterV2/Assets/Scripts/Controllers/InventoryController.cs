using System.Collections.Generic;
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

        if (!_bullets.ContainsKey(weapon.BulletType))
        {
            _bullets.Add(weapon.BulletType, 0);
        }
    }

    public void AddBullets(BulletsMagazineModel bullets)
    {
        if (_bullets.ContainsKey(bullets.BulletType))
        {
            _bullets[bullets.BulletType] += bullets.BulletCount;
        }
        else
        {
            _bullets.Add(bullets.BulletType, bullets.BulletCount);
        }
    }

    public void ShowBulletsCount()
    {
        if (_choosenWeapon == null)
        {
            _bulletsUImodel.SetBulletsCount("");
            return;
        }

        _bulletsUImodel.SetBulletsCount($"{_choosenWeapon.Bullets} / {_bullets[_choosenWeapon.BulletType]}");
    }

    public void StartReload()
    {
        if (_choosenWeapon != null && !_isReloading)
        {
            if (_bullets[_choosenWeapon.BulletType] > 0 && _choosenWeapon.Bullets < _choosenWeapon.MaxBullets)
            {
                _isReloading = true;
                new TimeController(Reload, 1f / _choosenWeapon.ReloadSpeed, false);
                ServiceLocator.GetService<WeaponController>().Wait(1f / _choosenWeapon.ReloadSpeed);
            }

        }
    }

    private void Reload()
    {
        for (int i = 0; i < _choosenWeapon.MaxBullets; i++)
        {
            _choosenWeapon.Bullets++;
            _bullets[_choosenWeapon.BulletType]--;

            if (_bullets[_choosenWeapon.BulletType] == 0 || _choosenWeapon.Bullets == _choosenWeapon.MaxBullets)
            {
                break;
            }
        }
        _isReloading = false;
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

    #endregion
}
