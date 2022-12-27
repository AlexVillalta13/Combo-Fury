using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction touchPressedAction;
    [SerializeField] GameEvent touchPressedEvent;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchPressedAction = playerInput.actions.FindAction("TouchPressed");
    }

    private void OnEnable()
    {
        touchPressedAction.performed += TouchPressed;
    }

    private void OnDisable()
    {
        touchPressedAction.performed -= TouchPressed;
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {
        touchPressedEvent.Raise();
    }
}
