using System;
using System.IO;
using System.Xml.Serialization;


public class XMLdata<T> : IData<T>
{
    #region Fields

    private static XmlSerializer _formatter;

    #endregion


    #region ClassLifeCycles

    public XMLdata()
    {
        _formatter = new XmlSerializer(typeof(T));
    }


    #endregion


    #region Methods

    public T Load(string path)
    {
        T result;
        if (!File.Exists(path)) return default(T);
        using (var fs = new FileStream(path, FileMode.Open))
        {
            result = (T)_formatter.Deserialize(fs);
        }
        return result;
    }

    public void Save(T data, string path = null)
    {
        if (data != null && !string.IsNullOrEmpty(path))
        {
            using (var fs = new FileStream(path, FileMode.Create))
            {
                _formatter.Serialize(fs, data);
            }
        }
    }

    #endregion

}
