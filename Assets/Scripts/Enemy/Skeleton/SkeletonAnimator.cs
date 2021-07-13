using UnityEngine;

[RequireComponent(typeof(WaypointMovement))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class SkeletonAnimator : MonoBehaviour
{
    private WaypointMovement _waypointMovement;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private readonly string _walk = "Walk";

    private void Awake()
    {
        _waypointMovement = GetComponent<WaypointMovement>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _waypointMovement.Walked += OnWalked;
        _waypointMovement.LookRight += OnLookRight;
    }

    private void OnWalked(bool isWalked)
    {
        _animator.SetBool(_walk, isWalked);
    }

    private void OnLookRight(bool isLookRight)
    {
        if (_spriteRenderer.flipX == false && isLookRight == false || _spriteRenderer.flipX && isLookRight)
        {
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }
    }
}
