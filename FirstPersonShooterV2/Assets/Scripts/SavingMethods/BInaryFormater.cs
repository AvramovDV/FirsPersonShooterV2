using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class BInaryFormater<T> : IData<T>
{
    private BinaryFormatter _formatter;

    #region ClassLifeCycles

    public BInaryFormater()
    {
        _formatter = new BinaryFormatter();
    }

    #endregion


    #region IData<T>

    public T Load(string path)
    {
        T result;
        if (!File.Exists(path)) return default(T);
        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            result = (T)_formatter.Deserialize(fs);
        }
        return result;
    }

    public void Save(T data, string path = null)
    {
        if (data != null && !string.IsNullOrEmpty(path))
        {
            if (typeof(T).IsSerializable)
            {
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    _formatter.Serialize(fs, data);
                }
            }
        }
    }

    #endregion

}
