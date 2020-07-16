using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class BotsController : BaseController
{
    #region Fields

    private List<List<Vector3>> _paths = new List<List<Vector3>>();
    private List<BotModel> _bots;
    private GameObject _botPrefub;

    #endregion


    #region ClassLifeCycles

    public BotsController()
    {
        FindAndSetPaths();
        SetBots();
        On();
    }

    #endregion


    #region Methods

    public override void On()
    {
        base.On();
        ServiceLocatorMonoBehaviour.GetService<GameController>().OnUpdate += BotsLogic;
    }

    public override void Off()
    {
        base.Off();
        ServiceLocatorMonoBehaviour.GetService<GameController>().OnUpdate -= BotsLogic;
    }

    private void BotsLogic()
    {
        for (int i = 0; i < _bots.Count; i++)
        {
            _bots[i].Execute();
        }
    }

    private void FindAndSetPaths()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(TagsManager.PathTag);

        for (int i = 0; i < objects.Length; i++)
        {
            List<Vector3> points = (from a in objects[i].GetComponentsInChildren<Transform>() select a.position).ToList();
            points.RemoveAt(0);
            _paths.Add(points);
        }
    }

    private void SetBots()
    {
        _botPrefub = Resources.Load<GameObject>(StaticData.BotPrefubPath);
        _bots = new List<BotModel>();

        for (int i = 0; i < _paths.Count; i++)
        {
            BotModel bot = GameObject.Instantiate(_botPrefub, _paths[i][0], _botPrefub.transform.rotation).GetComponent<BotModel>();
            bot.Path = _paths[i];
            _bots.Add(bot);
        }
    }

    public void RemoveBot(BotModel bot)
    {
        _bots.Remove(bot);
    }

    #endregion
}
