using UnityEngine;


public class BotPartModel : MonoBehaviour, ISelectable, ISetDamege
{
    #region Fields

    [SerializeField] private BotModel _botModel;
    [SerializeField] private float _damageModifier;

    #endregion


    #region ISelectable

    public string GetName()
    {
        return _botModel.name;
    }

    #endregion


    #region ISetDamege

    public void SetDamage(float damage)
    {
        _botModel.SetDamage(damage * _damageModifier);
    }

    #endregion
}
