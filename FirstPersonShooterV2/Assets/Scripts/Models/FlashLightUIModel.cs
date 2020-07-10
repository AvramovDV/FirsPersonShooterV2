using UnityEngine.UI;
using UnityEngine;

public class FlashLightUIModel : MonoBehaviour
{
    #region Fields

    [SerializeField] private Image _energyBar;
    [SerializeField] private Text _energyText;

    #endregion


    #region Methods

    public void SetEnergy(float energy)
    {
        _energyBar.fillAmount = energy;
        _energyText.text = (energy * 100).ToString("F1");
    }

    #endregion
}
