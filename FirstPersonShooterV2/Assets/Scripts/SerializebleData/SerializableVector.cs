using System;
using UnityEngine;

[Serializable]
public struct SerializableVector 
{
    #region Fields

    public float X;
    public float Y;
    public float Z;

    #endregion


    #region ClassLifeCycles

    public SerializableVector(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    #endregion


    #region Methods

    public static implicit operator Vector3(SerializableVector value)
    {
        return new Vector3(value.X, value.Y, value.Z);
    }

    public static implicit operator SerializableVector(Vector3 value)
    {
        return new SerializableVector(value.x, value.y, value.z);
    }

    public override string ToString()
    {
        return $"X = {X}; Y = {Y}; Z = {Z}";
    }

    #endregion
}
