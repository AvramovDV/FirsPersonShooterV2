using UnityEngine;


public class InputController : BaseController
{
    #region Fields

    private KeyCode _flashLightSwitch = KeyCode.F;
    private KeyCode _interactKey = KeyCode.E;
    private KeyCode _reloadGunKey = KeyCode.R;
    private KeyCode _dropDownGunKey = KeyCode.Q;
    private KeyCode _weaponOneKey = KeyCode.Alpha1;
    private KeyCode _weaponTwoKey = KeyCode.Alpha2;
    private KeyCode _weaponThreeKey = KeyCode.Alpha3;
    private KeyCode _weaponFourKey = KeyCode.Alpha4;

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

        WeaponChoosing();

    }

    private void WeaponChoosing()
    {
        if (Input.GetKeyDown(_weaponOneKey))
        {
            ServiceLocator.GetService<InventoryController>().ChooseWeapon(0);
        }

        if (Input.GetKeyDown(_weaponTwoKey))
        {
            ServiceLocator.GetService<InventoryController>().ChooseWeapon(1);
        }

        if (Input.GetKeyDown(_weaponThreeKey))
        {
            ServiceLocator.GetService<InventoryController>().ChooseWeapon(2);
        }

        if (Input.GetKeyDown(_weaponFourKey))
        {
            ServiceLocator.GetService<InventoryController>().ChooseWeapon(3);
        }

        if (Input.mouseScrollDelta.y != 0f)
        {
            if (Input.mouseScrollDelta.y > 0f)
            {
                ServiceLocator.GetService<InventoryController>().ChooseWeapon(1, false);
            }
            else if (Input.mouseScrollDelta.y < 0f)
            {
                ServiceLocator.GetService<InventoryController>().ChooseWeapon(-1, false);
            }
        }
    }

    #endregion
}
