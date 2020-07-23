using System;
using UnityEngine;

[Serializable]
public struct SerializableQuanternion
{
    #region Fields

    public float X;
    public float Y;
    public float Z;
    public float W;

    #endregion


    #region ClassLifeCycles

    public SerializableQuanternion(float x, float y, float z, float w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    #endregion


    #region Methods

    public static implicit operator Quaternion(SerializableQuanternion value)
    {
        return new Quaternion(value.X, value.Y, value.Z, value.W);
    }

    public static implicit operator SerializableQuanternion(Quaternion value)
    {
        return new SerializableQuanternion(value.x, value.y, value.z, value.w);
    }

    #endregion
}
