using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsController : MonoBehaviour
{
    Animator m_animator;
    [SerializeField] ParticleSystem m_GetHitParticleSystem;
    [SerializeField] ParticleSystem m_GetCriticalHitParticleSystem;
    [SerializeField] ParticleSystem m_EnemyDeathParticleSystem;

    [SerializeField] List<string> attackAnimations = new List<string>();
    [SerializeField] List<string> criticalAttackAnimations = new List<string>();
    [SerializeField] string getHitAnimation = "GetHit";
    [SerializeField] string enemyDeathAnimation = "EnemyLose";
    [SerializeField] string enemyWinsAnimation = "EnemyWins";

    private void Awake()
    {
        m_animator = GetComponentInChildren<Animator>();
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

    public void GetHitAnimation()
    {
        m_animator.SetTrigger(getHitAnimation);
        m_GetHitParticleSystem.Play();
    }

    public void GetCriticalHitAnimation()
    {
        m_animator.SetTrigger(getHitAnimation);
        m_GetCriticalHitParticleSystem.Play();
    }

    public void EnemyDeathAnimation()
    {
        m_animator.SetTrigger(enemyDeathAnimation);
    }

    public void EnemyWinsAnimation()
    {

        //if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("Base.Die"))
        //{
        //    return;
        //}
        m_animator.SetTrigger(enemyWinsAnimation);
    }
}
