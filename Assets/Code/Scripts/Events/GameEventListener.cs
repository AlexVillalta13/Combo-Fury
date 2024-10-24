using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] GameEvent gameEvent;
    [SerializeField] UnityEvent unityEvent;

    private void OnEnable()
    {
        gameEvent.RegisterEventListener(this);
    }

    private void OnDisable()
    {
        gameEvent.UnregisterEventListener(this);
    }

    public void OnEventRaised()
    {
        //Debug.Log(": " + gameEvent.name);
        unityEvent.Invoke();
    }
}
