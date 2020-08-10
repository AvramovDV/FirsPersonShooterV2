using UnityEngine;


public class PistolModel : BaseWeaponModel, ISelectable, IInteractable
{
    #region Methods

    public override void Fire()
    {
        if (Bullets > 0)
        {
            BulletModel bullet = Instantiate(BulletPrefub, FirePoint.position, FirePoint.rotation).GetComponent<BulletModel>();
            bullet.Damage = Damage;
            Bullets--;
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
