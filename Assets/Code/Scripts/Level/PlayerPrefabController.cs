using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefabController : MonoBehaviour
{
    private Rigidbody m_rigidbody;
    [SerializeField] float walkingVelocity = 5f;
    float currentVelocity = 0f;
    float nextPositionToGO = 0f;

    [SerializeField] GameEvent enemyEncountered;
    [SerializeField] GameEvent beginWalkEvent;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        nextPositionToGO = transform.position.x + 10f;
    }

    //private void OnEnable()
    //{
    //    beginWalkEvent.Raise();
    //}

    private void Update()
    {
        Walk();
    }

    private void Walk()
    {
        Vector3 vel = Vector3.right * currentVelocity;
        float yVel = m_rigidbody.velocity.y;
        vel.y = yVel;
        m_rigidbody.velocity = vel;
        if(transform.position.x >= nextPositionToGO)
        {
            StopWalk();
            enemyEncountered.Raise();
            UpdateNextPositionToGo();
        }
    }

    public void StartWalk()
    {
        currentVelocity = walkingVelocity;
    }

    public void StopWalk()
    {
        currentVelocity = 0f;
    }

    public void UpdateNextPositionToGo()
    {
        nextPositionToGO += 10f;
    }
}
