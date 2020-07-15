using UnityEngine;


public class InputController : BaseController
{
    #region Fields

    private KeyCode _flashLightSwitch = KeyCode.F;
    private KeyCode _interactKey = KeyCode.E;
    private KeyCode _reloadGunKey = KeyCode.R;
    private KeyCode _dropDownGunKey = KeyCode.Q;
    private MouseButton _fireButton = MouseButton.Left;

    #endregion


    #region ClassLifeCycles

    public InputController()
    {
        On();
    }

    #endregion


    #region Methods

    public override void On()
    {
        base.On();
        ServiceLocatorMonoBehaviour.GetService<GameController>().OnUpdate += InputLogic;
    }

    private void InputLogic()
    {
        if (Input.GetKeyDown(_flashLightSwitch))
        {
            ServiceLocator.GetService<FlashLightController>().Switch();
        }

        if (Input.GetKeyDown(_interactKey))
        {
            ServiceLocator.GetService<SelectionController>().Interact();
        }

        if (Input.GetMouseButton((int) _fireButton))
        {
            ServiceLocator.GetService<WeaponController>().Fire();
        }

        if (Input.GetKeyDown(_reloadGunKey))
        {
            ServiceLocator.GetService<WeaponController>().Reload();
        }

        if (Input.GetKeyDown(_dropDownGunKey))
        {
            ServiceLocator.GetService<InventoryController>().DropDownWeapon();
        }
    }

    #endregion
}
