using System;

using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] [Range(0f, 10f)] private float _speed = 3f;
    [SerializeField] [Range(0f, 1f)] private float _epsilon = 1f;

    private Transform[] _points;
    private Transform _targetPoint;
    private int _currentPoint;

    public event Action<bool> Walked;
    public event Action<bool> LookRight;

    private void Start()
    {
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
            _points[i] = _path.GetChild(i);

        _targetPoint = _points[_currentPoint];
    }

    private void Update()
    {
        Walked?.Invoke(transform.position.x != _targetPoint.position.x);

        transform.position = Vector3.MoveTowards(transform.position, _targetPoint.position, _speed * Time.deltaTime);

        if (transform.position.x < _targetPoint.position.x)
            LookRight?.Invoke(true);
        else if (transform.position.x > _targetPoint.position.x)
            LookRight?.Invoke(false);

        if (Mathf.Abs(Vector3.Distance(transform.position, _targetPoint.position)) <= _epsilon)
            _targetPoint = _points[GetNextPoint()];
    }

    private int GetNextPoint()
    {
        _currentPoint++;

        if (_currentPoint >= _points.Length)
        {
            _currentPoint %= _points.Length;
        }

        return _currentPoint;
    }
}
