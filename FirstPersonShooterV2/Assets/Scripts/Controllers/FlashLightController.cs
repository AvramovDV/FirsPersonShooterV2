using UnityEngine;


public class FlashLightController : BaseController
{
    #region Fields

    private FlashLightModel _model;
    private FlashLightUIModel _modelUI;

    #endregion


    #region ClassLifeCycle

    public FlashLightController()
    {
        _model = Object.FindObjectOfType<FlashLightModel>();
        _modelUI = Object.FindObjectOfType<FlashLightUIModel>();
        Off();
    }

    #endregion


    #region Methods

    public override void On()
    {
        base.On();
        ServiceLocatorMonoBehaviour.GetService<GameController>().OnUpdate += LoseEnergy;
        ServiceLocatorMonoBehaviour.GetService<GameController>().OnUpdate -= GrowEnergy;
        _model.Light.enabled = true;
    }

    public override void Off()
    {
        base.Off();
        ServiceLocatorMonoBehaviour.GetService<GameController>().OnUpdate -= LoseEnergy;
        ServiceLocatorMonoBehaviour.GetService<GameController>().OnUpdate += GrowEnergy;
        _model.Light.enabled = false;
    }

    private void LoseEnergy()
    {
        _model.Energy -= _model.LoseEnergySpeed * Time.deltaTime;
        Blinking();
        if (_model.Energy <= 0f)
        {
            Off();
        }
        ShowEnergy();
    }

    private void GrowEnergy()
    {
        _model.Energy += _model.GrowEnergySpeed * Time.deltaTime;
        ShowEnergy();
    }

    private void Blinking()
    {
        if (_model.Energy / _model.MaxEnergy < _model.BlinkingThreshold)
        {
            _model.Light.enabled = Random.Range(0, 100) > Random.Range(0, 10);
        }
    }

    private void ShowEnergy()
    {
        _modelUI.SetEnergy(_model.Energy / _model.MaxEnergy);
    }

    #endregion
}
