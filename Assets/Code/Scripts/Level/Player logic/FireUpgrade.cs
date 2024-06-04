using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireUpgrade : UpgradeBehaviour
{
    private bool isEnemyOnFire = false;
    private float fireTimer = 0f;
    private float fireDamageTimer = 0f;

    [SerializeField] GameEvent OnActivateEnemyInFire;
    [SerializeField] GameEvent onDeactivateEnemyInFire;

    private void Update()
    {
        if(isEnemyOnFire == true)
        {
            if (fireTimer <= 0f)
            {
                TurnOffFire();
            }
            if (fireDamageTimer <= 0f)
            {
                DoFireDamage();
            }

            fireTimer -= Time.deltaTime;
            fireDamageTimer -= Time.deltaTime;
        }
    }

    private void DoFireDamage()
    {
        fireDamageTimer = inCombatPlayerStatsSO.TimeToDoDamageFire;
        PlayerAttacks.OnPlayerAttacks?.Invoke(this, new PlayerAttacks.OnPlayerAttacksEventArgs() { playerAttackDamage = CalculateFireDamage() });
    }

    private float CalculateFireDamage()
    {
        return inCombatPlayerStatsSO.FirePercentageDamage / 100f * inCombatPlayerStatsSO.Attack;
    }

    public void TurnOffFire()
    {
        isEnemyOnFire = false;
        onDeactivateEnemyInFire.Raise(gameObject);
        fireTimer = 0f;
        fireDamageTimer = inCombatPlayerStatsSO.TimeToDoDamageFire;
    }

    public void FireUpgradeSelected()
    {
        inCombatPlayerStatsSO.LevelUpFireAttack();
    }

    public void OnFireAttack()
    {
        if(HasUpgrade())
        {
            isEnemyOnFire = true;
            fireTimer = inCombatPlayerStatsSO.TimeToTurnOffFire;
            OnActivateEnemyInFire.Raise(gameObject);
        }
    }
}
