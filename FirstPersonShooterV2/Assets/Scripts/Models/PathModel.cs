using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PathModel : MonoBehaviour
{
    #region Fields

    [SerializeField] private bool _loop;
    private List<Transform> _points = new List<Transform>();

    #endregion


    #region Propeties

    public List<Transform> Points { get => _points; }
    public bool Loop { get => _loop; }

    #endregion


    #region Methods

    public Vector3[] GetPoints()
    {
        _points = new List<Transform>();
        _points = transform.GetComponentsInChildren<Transform>().ToList<Transform>();
        _points.Remove(transform);

        if (_loop)
        {
            _points.Add(_points[0]);
        }

        return (from a in _points select a.position).ToArray<Vector3>();
    }

    public void AddPoint()
    {
        Transform newPoint = new GameObject($"PathPoint({_points.Count})").transform;
        if (_points.Count > 1)
        {
            newPoint.position = Vector3.Lerp(_points[0].position, _points[(_loop ? _points.Count - 2 : _points.Count - 1)].position, 0.5f);
        }
        else
        {
            newPoint.position += Vector3.forward * 5f;
        }
        newPoint.parent = transform;
        _points.Add(newPoint);

    }

    #endregion
}
