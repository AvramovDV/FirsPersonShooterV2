using UnityEngine;


public class InputController : BaseController
{
    #region Fields

    private KeyCode _flashLightSwitch = KeyCode.F;

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
    }

    #endregion
}
