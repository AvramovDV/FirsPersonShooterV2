using UnityEngine;


public class Controllers
{
    #region ClassLifeCycles

    public Controllers()
    {
        ServiceLocator.SetService<PlayerController>(new PlayerController());
        ServiceLocator.SetService<FlashLightController>(new FlashLightController());
        ServiceLocator.SetService<InputController>(new InputController());
    }

    #endregion
}
