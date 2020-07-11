using UnityEngine;
using UnityEngine.UI;


public class SelectionModel : MonoBehaviour
{
    #region Fields

    [SerializeField] private Text _selectionMessageText;

    #endregion


    #region Methods

    public void SetMessage(string message)
    {
        _selectionMessageText.text = message;
    }

    #endregion
}
