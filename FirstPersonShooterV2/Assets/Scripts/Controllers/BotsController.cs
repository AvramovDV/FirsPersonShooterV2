using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class BotsController : BaseController
{
    #region Fields

    private List<List<Vector3>> _paths = new List<List<Vector3>>();
    private List<BotModel> _bots;
    private GameObject _botPrefub;

    private int _botsCount = 20;
    private int _currentPath = 0;

    private TimeController _timeController;

    private Queue<BotModel> _resetBots;

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
        _bots = new List<BotModel>(_botsCount);
        _resetBots = new Queue<BotModel>(_botsCount);

        for (int i = 0; i < _botsCount; i++)
        {
            BotModel bot = GameObject.Instantiate(_botPrefub, _paths[_currentPath][0], _botPrefub.transform.rotation).GetComponent<BotModel>();
            bot.Path = _paths[_currentPath];
            _bots.Add(bot);
            _currentPath++;
            if (_currentPath >= _paths.Count)
            {
                _currentPath = 0;
            }
        }
    }

    public void RemoveBot(BotModel bot)
    {
        //_bots.Remove(bot);
        _resetBots.Enqueue(bot);
        new TimeController(ResetBot, bot.DieTime, false);
    }

    private void ResetBot()
    {
        BotModel bot = _resetBots.Dequeue();
        bot.Path = _paths[_currentPath];
        bot.Agent.Warp(bot.Path[0]);
        bot.SwitchState(BotState.None);

        _currentPath++;
        if (_currentPath >= _paths.Count)
        {
            _currentPath = 0;
        }
    }

    #endregion
}
