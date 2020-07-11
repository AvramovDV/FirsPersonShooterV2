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
        if (_inventoryController.ChoosenWeapon == null)
        {
            return;
        }

        if (IsActive)
        {
            Switch();
            _inventoryController.ChoosenWeapon.Fire();
            if (_timeController != null)
            {
                _timeController.Off();
            }
                _timeController = new TimeController(Switch, 1 / _inventoryController.ChoosenWeapon.FireSpeed, false);
        }

    }

    public void Wait(float time)
    {
        Off();
        if (_timeController != null)
        {
            _timeController.Off();
        }
        _timeController = new TimeController(Switch, time, false);
    }


    #endregion
}
