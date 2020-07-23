using System.IO;
using UnityEngine;


public class SaveLoadController : BaseController
{
    #region Fields

    private IData<SerializableObject> _data;

    private string _folderName = "SaveData";
    private string _fileName = "data.save";

    private readonly string _path;

    #endregion


    #region ClassLifeCycles

    public SaveLoadController()
    {
        //_data = new BInaryFormater<SerializableObject>();
        //_data = new JSON<SerializableObject>();
        _data = new XMLdata<SerializableObject>();
        _path = Path.Combine(Application.dataPath, _folderName);
    }

    #endregion


    #region Methods

    public void Save()
    {
        if (!Directory.Exists(Path.Combine(_path)))
        {
            Directory.CreateDirectory(_path);
        }

        var player = new SerializableObject(Object.FindObjectOfType<PlayerModel>().gameObject);

        _data.Save(player, Path.Combine(_path, _fileName));
    }

    public void Load()
    {
        var file = Path.Combine(_path, _fileName);
        if (!File.Exists(file)) return;
        var newPlayer = _data.Load(file);

        GameObject playerModel = Object.FindObjectOfType<PlayerModel>().gameObject;

        playerModel.name = newPlayer.Name;
        playerModel.transform.position = newPlayer.Position;
        playerModel.transform.rotation = newPlayer.Rotation;
        playerModel.transform.localScale = newPlayer.Scale;
        playerModel.SetActive(newPlayer.IsEnable);
    }

    #endregion
}
