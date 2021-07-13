using UnityEngine;

[RequireComponent(typeof(CrystalMovement))]
[RequireComponent(typeof(Animator))]
public class CrystalAnimator : MonoBehaviour
{
    private CrystalMovement _crystalMovement;
    private Animator _animator;

    private readonly string _collected = "Collected";

    private void Awake()
    {
        _crystalMovement = GetComponent<CrystalMovement>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _crystalMovement.Collected += OnCollected;
    }

    private void OnCollected()
    {
        _animator.SetTrigger(_collected);
    }
}
