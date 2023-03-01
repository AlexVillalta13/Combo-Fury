using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsController : MonoBehaviour
{
    Animator m_animator;

    [SerializeField] List<string> attackAnimations = new List<string>();
    [SerializeField] List<string> criticalAttackAnimations = new List<string>();
    //[SerializeField] string deathAnimation = "EnemyLose";

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    public void SelectRandomAttackAnimation()
    {
        int index = Random.Range(0, attackAnimations.Count);
        m_animator.SetTrigger(attackAnimations[index]);
    }

    public void SelectRandomCriticalAttack()
    {
        int index = Random.Range(0, criticalAttackAnimations.Count);
        m_animator.SetTrigger(criticalAttackAnimations[index]);
    }

    //public void DieAnimation()
    //{
    //    m_animator.SetTrigger(deathAnimation);
    //}
}
