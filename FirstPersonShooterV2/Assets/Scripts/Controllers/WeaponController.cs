using System.Diagnostics;
using UnityEngine;


public class WeaponController : BaseController
{
    #region Fields

    private InventoryController _inventoryController;
    private TimeController _timeController;

    #endregion


    #region ClassLifeCycles

    public WeaponController()
    {
        _inventoryController = ServiceLocator.GetService<InventoryController>();
        On();
    }

    #endregion


    #region Methods


    public void Fire()
    {
        if (_inventoryController.ChoosenWeapon != null)
        {
            _inventoryController.ChoosenWeapon.Fire();
        }
    }

    public void Reload()
    {
        if (_inventoryController.ChoosenWeapon != null)
        {
            _inventoryController.ChoosenWeapon.Reload();
        }
    }

    #endregion
}
