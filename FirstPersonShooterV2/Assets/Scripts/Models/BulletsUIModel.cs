using UnityEngine;
using UnityEngine.UI;


public class BulletsUIModel : MonoBehaviour
{
    #region Fields

    [SerializeField] private Text _bulletsCount;

    #endregion


    #region Methods

    public void SetBulletsCount(string text)
    {
        _bulletsCount.text = text;
    }

    #endregion
}
