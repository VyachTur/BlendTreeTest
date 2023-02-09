using System;
using UnityEngine;

namespace Scripts
{
  public class PlayerAttack : MonoBehaviour
  {
    public event Action OnPlayerAttackEvent;

    private const string AttackHand = "DoAttack1";
    private const string AttackFoot = "DoAttack2";

    [SerializeField] private Animator _playerAnimator;

    private PlayerMove _playerMove;

    private void Start()
    {
        TryGetComponent(out _playerMove);
    }

    private void Update()
    {
      if (_playerMove.IsPlayerJump) return;

      if (Input.GetMouseButtonDown(0))
      {
        _playerAnimator.SetTrigger(AttackHand);
        OnPlayerAttackEvent?.Invoke();
      }

      if (Input.GetMouseButtonDown(1))
      {
        _playerAnimator.SetTrigger(AttackFoot);
        OnPlayerAttackEvent?.Invoke();
      }
    }
  }
}

