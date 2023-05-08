using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NumberDisplayListener : MonoBehaviour
{
    [SerializeField] bool isPlayer = false;
    [SerializeField] Color enemyHitColor = Color.white;
    [SerializeField] Color enemyGetCriticalHitColor = Color.yellow;
    [SerializeField] Color playerHitColor = Color.red;
    [SerializeField] Color playerHealsColor = Color.green;

    Vector3 numberLocalStartPosition = new Vector3(1.2f, 1f, 0.4f);
    float minY = 0.5f;
    float maxY = 1.5f;
    float minZ = 0f;
    float maxZ = 0.7f;
    NumberDisplayPool pool;

    private void Awake()
    {
        pool = FindObjectOfType<NumberDisplayPool>();

        if(isPlayer == false)
        {
            numberLocalStartPosition.x *= -1;
        }
    }
    private void OnEnable()
    {
        if(isPlayer)
        {
            CombatController.onChangePlayerHealth += ShowNumber;
        }
        else
        {
            CombatController.onChangeEnemyHealth += ShowNumber;
        }
    }

    private void OnDisable()
    {
        if(isPlayer)
        {
            CombatController.onChangePlayerHealth -= ShowNumber;
        }
        else
        {
            CombatController.onChangeEnemyHealth -= ShowNumber;
        }
    }

    private void ShowNumber(float currentHealth, float maxHealth, float healthDifference)
    {
        if (healthDifference == 0) { return; }

        TextMeshPro number = pool.NumberMeshPool.Get();
        number.fontSize = 5;
        if (isPlayer == false)
        {
            number.color = enemyHitColor;
        }
        else
        {
            if (healthDifference > 0)
            {
                number.color = playerHealsColor;
            }
            else if (healthDifference < 0)
            {
                number.color = playerHitColor;
                healthDifference *= -1;
            }
        }

        PositionText(number);

        number.text = healthDifference.ToString();

        number.transform.SetParent(transform);
    }

    private void PositionText(TextMeshPro number)
    {
        float randomY = Random.Range(minY, maxY);
        float randomZ = Random.Range(minZ, maxZ);
        numberLocalStartPosition.y = randomY;
        numberLocalStartPosition.z = randomZ;
        number.transform.position = transform.TransformPoint(numberLocalStartPosition);
    }

    public void ShowText(string textToShow)
    {
        if (isPlayer == true)
        {
            TextMeshPro textMesh = pool.NumberMeshPool.Get();
            textMesh.color = Color.white;

            PositionText(textMesh);

            textMesh.text = textToShow;
            textMesh.fontSize = 3;
        }
    }
}
