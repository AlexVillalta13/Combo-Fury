using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class NumberDisplayPool : MonoBehaviour
{
    private ObjectPool<TextMeshPro> numberMeshPool;
    [SerializeField] TextMeshPro numberDisplayPrefab;
    public ObjectPool<TextMeshPro> NumberMeshPool 
    { 
        get 
        {
            return numberMeshPool; 
        } 
    }
    private void Awake()
    {
        if(numberMeshPool == null)
        {
            numberMeshPool = new ObjectPool<TextMeshPro>(CreatePoolItem, OnTakeItemFromPool, OnReturnObjectToPool, defaultCapacity: 10);
        }
    }

    private TextMeshPro CreatePoolItem()
    {
        TextMeshPro numberText = Instantiate(numberDisplayPrefab);
        numberText.GetComponent<NumberDisplayAnimation>().SetPool(this);
        return numberText;
    }

    private void OnTakeItemFromPool(TextMeshPro numberText)
    {
        numberText.gameObject.SetActive(true);
    }

    private void OnReturnObjectToPool(TextMeshPro numberText)
    {
        numberText.transform.SetParent(null);
        numberText.gameObject.SetActive(false);
    }
}
