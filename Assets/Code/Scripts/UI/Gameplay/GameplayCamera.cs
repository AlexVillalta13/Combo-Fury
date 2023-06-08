using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayCamera : MonoBehaviour
{
    CinemachineVirtualCamera gameplayCamera;

    private void Awake()
    {
        gameplayCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void SetTarget(Transform target)
    {
        gameplayCamera.Follow = target;
        gameplayCamera.LookAt = target;
    }
}
