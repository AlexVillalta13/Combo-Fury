using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefabController : MonoBehaviour
{
    private Rigidbody m_rigidbody;
    [SerializeField] float walkingVelocity = 5f;
    float currentVelocity = 0f;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        StartWalk();
    }

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
    }

    public void StartWalk()
    {
        currentVelocity = walkingVelocity;
    }

    public void StopWalk()
    {
        currentVelocity = 0f;
    }
}
