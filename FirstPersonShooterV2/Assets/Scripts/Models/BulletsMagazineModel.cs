using UnityEngine;


public class BulletsMagazineModel : MonoBehaviour, ISelectable, IInteractable
{
    #region Fields

    [SerializeField] private BulletType _bulletType;
    [SerializeField] private string _name;
    [SerializeField] private int _bulletCount;

    #endregion


    #region Propeties

    public BulletType BulletType { get => _bulletType; }
    public int BulletCount { get => _bulletCount; }

    #endregion


    #region ISelectable

    public string GetName()
    {
        return _name;
    }

    #endregion


    #region IInteractable

    public void Interact()
    {
        ServiceLocator.GetService<InventoryController>().AddBullets(this);
        Destroy(gameObject);
    }

    #endregion

}
