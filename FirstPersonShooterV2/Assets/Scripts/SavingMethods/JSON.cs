using System.IO;
using UnityEngine;


public class JSON<T> : IData<T>
{
    #region Methods

    public T Load(string path)
    {
        string jsonString = File.ReadAllText(path);
        return (T)JsonUtility.FromJson<T>(jsonString);
    }

    public void Save(T data, string path = null)
    {
        string jsonString = JsonUtility.ToJson(data);
        File.WriteAllText(path, jsonString);
    }

    #endregion

}
