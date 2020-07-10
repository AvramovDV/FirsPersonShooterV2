using System;
using UnityEngine;


public class GameController : MonoBehaviour
{
    #region Propeties

    public Action OnUpdate = delegate { };

    #endregion


    #region UnityMethods

    private void Start()
    {
        new Controllers();
    }

    private void Update()
    {
        OnUpdate.Invoke();
    }

    #endregion
}
