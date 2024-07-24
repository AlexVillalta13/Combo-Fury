using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardCollector : MonoBehaviour
{
    private int goldReward;

    [SerializeField] EnemyStats EnemyStats;

    public void AddReward()
    {
        foreach (CurrencyReward reward in EnemyStats.currencyRewardList)
        {
            if(reward.Currency.name == "Gold")
            {
                goldReward += reward.Amount;
            }
        }
    }

    public void ResetRewards()
    {
        goldReward = 0;
    }
}