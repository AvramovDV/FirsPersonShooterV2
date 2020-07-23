using System;
using UnityEngine;


[Serializable]
public class SerializableObject
{
    #region Fields

    public string Name;
    public SerializableVector Position;
    public SerializableQuanternion Rotation;
    public SerializableVector Scale;
    public bool IsEnable;

    #endregion


    #region ClassLifeCycles

    public SerializableObject(GameObject gameObject)
    {
        Name = gameObject.name;
        Position = gameObject.transform.position;
        Rotation = gameObject.transform.rotation;
        Scale = gameObject.transform.localScale;
        IsEnable = gameObject.activeInHierarchy;
    }

    #endregion


    #region Methods

    public override string ToString()
    {
        return $"Name = {Name}; IsEnable = {IsEnable}; Position = {Position}";
    }

    #endregion

}
