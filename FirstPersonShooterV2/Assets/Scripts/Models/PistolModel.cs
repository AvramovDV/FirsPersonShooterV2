using UnityEngine;


public class PistolModel : BaseWeaponModel, ISelectable, IInteractable
{
    #region Methods

    public override void Fire()
    {
        if (Bullets > 0 && CanFire)
        {
            Vector3 dir = ServiceLocator.GetService<SelectionController>().HitPoint - FirePoint.position;
            BulletModel bullet = Instantiate(BulletPrefub, FirePoint.position, Quaternion.LookRotation(dir)).GetComponent<BulletModel>();
            bullet.Damage = Damage;
            Bullets--;
            SwitchCanFire();
            new TimeController(SwitchCanFire, 1f / FireSpeed, false);
        }

    }

    public override void Reload()
    {
        SwitchCanFire();

        for (int i = 0; i < MaxBullets; i++)
        {
            if (ServiceLocator.GetService<InventoryController>().Bullets[BulletType] == 0 || Bullets == MaxBullets)
            {
                break;
            }
            Bullets++;
            ServiceLocator.GetService<InventoryController>().Bullets[BulletType]--;
        }

        new TimeController(SwitchCanFire, 1f / ReloadSpeed, false);
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
