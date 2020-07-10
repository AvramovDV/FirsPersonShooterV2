using System.Collections.Generic;
using UnityEngine;


public static class ServiceLocatorMonoBehaviour
{
    #region Fields

    private static Dictionary<object, object> _services = null;

    #endregion


    #region Methods

    public static T GetService<T>(bool createObjectIfNotFound = true) where T : Object
    {
        if (_services == null)
        {
            _services = new Dictionary<object, object>();
        }

        if (!_services.ContainsKey(typeof(T)))
        {
            return FindObject<T>(createObjectIfNotFound);
        }

        var a = (T) _services[typeof(T)];
        if (a != null)
        {
            return a;
        }

        _services.Remove(typeof(T));
        return FindObject<T>(createObjectIfNotFound);
    }

    private static T FindObject<T>(bool createObjectIfNotFound = true) where T : Object
    {
        T type = Object.FindObjectOfType<T>();
        if (type != null)
        {
            _services.Add(typeof(T), type);
        }
        else if (createObjectIfNotFound)
        {
            var a = new GameObject(typeof(T).Name, typeof(T));
            _services.Add(typeof(T), a.GetComponent<T>());
        }

        return (T)_services[typeof(T)];
    }

    #endregion
}
