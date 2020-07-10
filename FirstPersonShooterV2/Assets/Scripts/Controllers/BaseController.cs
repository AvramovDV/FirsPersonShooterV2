public class BaseController
{
    #region Propeties

    public bool IsActive { get; private set; }

    #endregion


    #region Methods

    public virtual void On()
    {
        IsActive = true;
    }

    public virtual void Off()
    {
        IsActive = false;
    }

    public virtual void Switch()
    {
        if (IsActive)
        {
            Off();
        }
        else
        {
            On();
        }
    }

    #endregion
}
