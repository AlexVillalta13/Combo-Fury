using System.Collections;
using System.Collections.Generic;
using TMPro;
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
            PlayerHealth.onChangePlayerHealth += ShowNumber;
        }
        else
        {
            EnemyHealth.onChangeEnemyHealth += ShowNumber;
        }
    }

    private void OnDisable()
    {
        if(isPlayer)
        {
            PlayerHealth.onChangePlayerHealth -= ShowNumber;
        }
        else
        {
            EnemyHealth.onChangeEnemyHealth -= ShowNumber;
        }
    }

    private void ShowNumber(object sender, OnChangeHealthEventArgs eventArgs)
    {
        if (eventArgs.spawnNumberTextMesh == false) { return; }

        TextMeshPro number = pool.NumberMeshPool.Get();
        number.fontSize = 5;
        if (isPlayer == false)
        {
            number.color = enemyHitColor;
        }
        else
        {
            if (eventArgs.healthDifference <= 0f)
            {
                number.color = playerHitColor;
            }
            else if (eventArgs.healthDifference > 0f)
            {
                number.color = playerHealsColor;
            }
            //else if(eventArgs.healthDifference == 0f)
            //{
            //    number.color = enemyHitColor;
            //}
        }

        PositionText(number);

        number.text = eventArgs.healthDifference.ToString("0");

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

    public void ShowWord(string wordToShow)
    {
        if (isPlayer == true)
        {
            TextMeshPro textMesh = pool.NumberMeshPool.Get();
            textMesh.color = Color.white;

            PositionText(textMesh);

            textMesh.text = wordToShow;
            textMesh.fontSize = 3;
        }
    }
}
