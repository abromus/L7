using System.Collections;

using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Crystal : MonoBehaviour
{
	private Animator _animator;
	private Vector3 _startPosition;
	private Vector3 _newPosition;
	private float _direction;

	private readonly float _speed = 1f;
	private readonly float _offsetY = 0.5f;
	private readonly float _up = 1;
	private readonly float _down = -1;
	private readonly string _collected = "Collected";

	private void Awake()
	{
		_animator = GetComponent<Animator>();
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
		if(collision.TryGetComponent<Player>(out _))
		{
			_animator.SetTrigger(_collected);
			Destroy(gameObject);
		}
	}

	private IEnumerator Move()
	{
		while(true)
		{
			transform.position = Vector3.MoveTowards(transform.position, _newPosition, _speed * Time.deltaTime);

			if(transform.position.y >= _startPosition.y + _offsetY || transform.position.y <= _startPosition.y - _offsetY)
			{
				_direction = _direction == _up ? _down : _up;
				_newPosition.y += _direction * _offsetY * 2;
			}

			yield return null;
		}
	}
}
