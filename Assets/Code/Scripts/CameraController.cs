using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera gameplayCamera;
    [SerializeField] CinemachineVirtualCamera mainMenuCamera;

    bool mainMenuEnabled = true;

    public void switchPriority()
    {
        mainMenuEnabled = !mainMenuEnabled;
        if (mainMenuEnabled == true)
        {
            mainMenuCamera.Priority = 1;
            gameplayCamera.Priority = 0;
        } else
        {
            mainMenuCamera.Priority = 0;
            gameplayCamera.Priority = 1;
        }
    }
}
