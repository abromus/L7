using System;
using System.Collections;

using UnityEngine;

public class CrystalMovement : MonoBehaviour
{
    [SerializeField] [Range(0f, 10f)] private float _speed = 1f;
    [SerializeField] private float _offsetY = 0.5f;

    private Vector3 _startPosition;
    private Vector3 _newPosition;
    private float _direction;

    private readonly float _up = 1;
    private readonly float _down = -1;

    public event Action Collected;

    private void Awake()
    {
        _startPosition = transform.position;

        _newPosition = transform.position;
        _newPosition.y += _offsetY;

        _direction = _up;
    }

    private void Start()
    {
        StartCoroutine(Move());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out _))
        {
            Collected?.Invoke();
            Destroy(gameObject);
        }
    }

    private IEnumerator Move()
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _newPosition, _speed * Time.deltaTime);

            if (transform.position.y >= _startPosition.y + _offsetY || transform.position.y <= _startPosition.y - _offsetY)
            {
                _direction = _direction == _up ? _down : _up;
                _newPosition.y += _direction * _offsetY * 2;
            }

            yield return null;
        }
    }
}
