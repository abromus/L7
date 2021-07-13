using System;

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Range(0f, 10f)] private float _speed = 4f;
    [SerializeField] [Range(0f, 10f)] private float _jumpForce = 5f;
    [SerializeField] [Range(0f, 10f)] private float _doubleJumpForce = 6.5f;
    [SerializeField] [Range(0f, 1f)] private float _checkRadius = 0.01f;
    [SerializeField] private Transform _substrate;
    [SerializeField] private LayerMask _whatIsGround;

    private Rigidbody2D _rigidbody2D;
    private bool _isGrounded = true;
    private bool _doubleJumpReady = false;
    private Vector3 _direction;
    private Vector3 _newPosition;

    public event Action<bool> Ran;
    public event Action<bool> LookRight;
    public event Action Jumped;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _newPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

        Run();
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(_substrate.position, _checkRadius, _whatIsGround);
    }

    private Vector3 GetDirection()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            return Vector3.left;
        else if (Input.GetKey(KeyCode.RightArrow))
            return Vector3.right;
        else
            return Vector3.zero;
    }

    private void Run()
    {
        _direction = GetDirection();
        _newPosition = transform.position + _direction;

        transform.position = Vector3.MoveTowards(transform.position, _newPosition, _speed * Time.deltaTime);

        if (_direction != Vector3.zero && _isGrounded)
            Ran?.Invoke(true);
        else
            Ran?.Invoke(false);

        if (_direction.x > 0)
            LookRight?.Invoke(true);
        else if (_direction.x < 0)
            LookRight?.Invoke(false);
    }

    private void Jump()
    {
        if (_isGrounded || _doubleJumpReady)
        {
            _rigidbody2D.velocity = _doubleJumpReady == false ? Vector2.up * _jumpForce : Vector2.up * _doubleJumpForce;
            _doubleJumpReady = !_doubleJumpReady;
            _isGrounded = !_isGrounded;

            Jumped?.Invoke();
        }
    }
}
