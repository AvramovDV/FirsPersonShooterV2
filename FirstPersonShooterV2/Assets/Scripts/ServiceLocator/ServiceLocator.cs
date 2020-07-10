using System;
using System.Collections.Generic;
using UnityEngine;


public static class ServiceLocator
{
    #region Fields

    private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

    #endregion


    #region Methods

    public static void SetService<T>(T value) where T : class
    {
        var type = typeof(T);
        if (!_services.ContainsKey(type))
        {
            _services[type] = value;
        }
    }

    public static T GetService<T>()
    {
        return (T)_services[typeof(T)];
    }

    #endregion
}
