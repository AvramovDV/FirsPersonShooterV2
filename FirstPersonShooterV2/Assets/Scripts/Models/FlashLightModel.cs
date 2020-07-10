using UnityEngine;


public class FlashLightModel : MonoBehaviour
{
    #region Fields

    [SerializeField] private Light _light;
    [SerializeField] private float _brightnes;
    [SerializeField] private float _maxEnergy;
    [SerializeField] private float _energy;
    [SerializeField] private float _loseEnergySpeed;
    [SerializeField] private float _growEnergySpeed;
    [SerializeField] private float _blinkingThreshold;

    #endregion


    #region Propeties

    public Light Light { get => _light; }
    public float MaxEnergy { get => _maxEnergy; }
    public float Energy 
    { 
        get => _energy;
        set
        {
            _energy = value;
            if (_energy < 0f)
            {
                _energy = 0f;
            }
            if (_energy > _maxEnergy)
            {
                _energy = _maxEnergy;
            }
            _light.intensity = _brightnes * _energy / _maxEnergy;
        }
    }
    public float LoseEnergySpeed { get => _loseEnergySpeed; }
    public float GrowEnergySpeed { get => _growEnergySpeed; }
    public float BlinkingThreshold { get => _blinkingThreshold; }

    #endregion

}
