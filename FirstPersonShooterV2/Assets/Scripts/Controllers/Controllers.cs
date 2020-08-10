using UnityEngine;


public class Controllers
{
    #region ClassLifeCycles

    public Controllers()
    {
        ServiceLocator.SetService<PlayerController>(new PlayerController());
        ServiceLocator.SetService<FlashLightController>(new FlashLightController());
        ServiceLocator.SetService<InputController>(new InputController());
        ServiceLocator.SetService<SelectionController>(new SelectionController());
        ServiceLocator.SetService<InventoryController>(new InventoryController());
        ServiceLocator.SetService<WeaponController>(new WeaponController());
        ServiceLocator.SetService<BotsController>(new BotsController());
        ServiceLocator.SetService<SaveLoadController>(new SaveLoadController());
    }

    #endregion
}
