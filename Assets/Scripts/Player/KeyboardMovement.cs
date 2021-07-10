using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class KeyboardMovement : MonoBehaviour
{
	[SerializeField] private Transform _substrate;
	[SerializeField] private LayerMask _whatIsGround;

	private Rigidbody2D _rigidbody2D;
	private SpriteRenderer _spriteRenderer;
	private Animator _animator;
	private bool _isGrounded = true;
	private bool _doubleJumpReady = false;
	private Vector3 _direction;
	private Vector3 _newPosition;

	private readonly float _speed = 4f;
	private readonly float _jumpForce = 5f;
	private readonly float _doubleJumpForce = 6.5f;
	private readonly float _checkRadius = 0.01f;
	private readonly string _run = "Run";
	private readonly string _jump = "Jump";

	private void Awake()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_animator = GetComponent<Animator>();
		_newPosition = transform.position;
	}

    private void Update()
    {
		if(Input.GetKeyDown(KeyCode.UpArrow))
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
		if(Input.GetKey(KeyCode.LeftArrow))
			return Vector3.left;
		else if(Input.GetKey(KeyCode.RightArrow))
			return Vector3.right;
		else
			return Vector3.zero;
	}

	private void Run()
	{
		_direction = GetDirection();
		_newPosition = transform.position + _direction;

		transform.position = Vector3.MoveTowards(transform.position, _newPosition, _speed * Time.deltaTime);

		if(_spriteRenderer.flipX == false && _direction.x > 0 || _spriteRenderer.flipX && _direction.x < 0)
		{
			Flip();
		}

		if(_direction != Vector3.zero && _isGrounded)
		{
			_animator.SetBool(_run, true);
		}
		else
		{
			_animator.SetBool(_run, false);
		}
	}

	private void Jump()
    {
		if(_isGrounded || _doubleJumpReady)
		{
			_rigidbody2D.velocity = _doubleJumpReady == false ? Vector2.up * _jumpForce : Vector2.up * _doubleJumpForce;
			_doubleJumpReady = !_doubleJumpReady;
			_isGrounded = !_isGrounded;

			_animator.SetTrigger(_jump);
		}
	}

    private void Flip()
    {
		_spriteRenderer.flipX = !_spriteRenderer.flipX;
	}
}
