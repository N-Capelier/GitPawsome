using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] Animator _animator;

    [Button("CatMove")]
    public void StartMove()
    {
        _animator.SetTrigger("CatMove");
    }

    [Button("StopMove")]
    public void EndMove()
    {
        _animator.SetTrigger("StopMove");
    }

    [Button("Attack")]
    public void Attack()
    {
        _animator.SetTrigger("Attack");
    }

    [Button("Spell")]
    public void Spell()
    {
        _animator.SetTrigger("Spell");
    }

    [Button("Hit")]
    public void Hit()
    {
        _animator.SetTrigger("Hit");
    }

    [Button("Death")]
    public void Death()
    {
        _animator.SetTrigger("Death");
    }

    [Button("StatsUp")]
    public void StatsUp()
    {
        _animator.SetTrigger("StatsUp");
    }

    [Button("StatsDown")]
    public void StatsDown()
    {
        _animator.SetTrigger("StatsDown");
    }
}
