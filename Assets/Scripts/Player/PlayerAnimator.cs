using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerAnimator : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private readonly string _run = "Run";
    private readonly string _jump = "Jump";

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _playerMovement.Ran += OnRan;
        _playerMovement.LookRight += OnLookRight;
        _playerMovement.Jumped += OnJumped;
    }

    private void OnRan(bool isRun)
    {
        _animator.SetBool(_run, isRun);
    }

    private void OnLookRight(bool isLookRight)
    {
        if (_spriteRenderer.flipX == false && isLookRight == false || _spriteRenderer.flipX && isLookRight)
        {
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }
    }

    private void OnJumped()
    {
        _animator.SetTrigger(_jump);
    }
}
