using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class WaypointMovement : MonoBehaviour
{
    [SerializeField] private Transform _path;

    private Transform[] _points;
    private SpriteRenderer _spriteRenderer;
    private int _currentPoint;

    private readonly float _epsilon = 1f;
    private readonly float _speed = 3f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void Start()
    {
        _points = new Transform[_path.childCount];

        for(int i = 0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
        }
    }

    private void Update()
    {
        Transform target = _points[_currentPoint];
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if(_spriteRenderer.flipX == false && transform.position.x < target.position.x || _spriteRenderer.flipX && transform.position.x > target.position.x)
        {
            Flip();
        }

        if(Mathf.Abs(Vector3.Distance(transform.position, target.position)) <= _epsilon)
        {
            _currentPoint++;

            if(_currentPoint >= _points.Length)
            {
                _currentPoint %= _points.Length;
            }
        }
    }

    private void Flip()
    {
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }
}
