using UnityEngine;


public class GrenadeModel : BaseWeaponModel, ISelectable, IInteractable
{
    #region Fields

    [SerializeField] private float _dropForce;

    #endregion


    #region Methods

    public override void Fire()
    {
        if (CanFire)
        {
            GameObject a = Instantiate(BulletPrefub, FirePoint.position, FirePoint.rotation);
            Vector3 direction = Vector3.Lerp(FirePoint.forward, FirePoint.up, 0.25f);
            a.GetComponent<Rigidbody>().AddForce(direction * _dropForce);
            a.GetComponent<GrenadeBulletModel>().Damage = Damage;
            Bullets--;
            Reload();
        }

    }

    public override void Reload()
    {
        if (ServiceLocator.GetService<InventoryController>().Bullets[BulletType] > 0)
        {
            Bullets++;
            ServiceLocator.GetService<InventoryController>().Bullets[BulletType]--;
            SwitchCanFire();
            new TimeController(SwitchCanFire, 1f / ReloadSpeed, false);
        }
        else
        {
            ServiceLocator.GetService<InventoryController>().DropDownWeapon();
            Destroy(gameObject);
        }
    }

    public void SwitchCanFire()
    {
        if (CanFire)
        {
            CanFire = false;
        }
        else
        {
            CanFire = true;
        }
    }

    #endregion


    #region ISelectable

    public string GetName()
    {
        return gameObject.name;
    }

    #endregion


    #region IInteractable

    public void Interact()
    {
        ServiceLocator.GetService<InventoryController>().AddWeapon(this);
    }

    #endregion

}
