using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 MoveDirection => inputActions.Player.Move.ReadValue<Vector2>();
    
    public bool IsMovePressed => MoveDirection != Vector2.zero;


    private InputActions inputActions;

    private void OnEnable()
    {
        EnableInput();
    }

    private void Awake()
    {
        inputActions = new InputActions();
    }

    private void OnDisable()
    {
        DisableInput();
    }

    public void EnableInput()
    {
        inputActions.Enable();
    }
    
    public void DisableInput()
    {
        inputActions.Disable();
    }
    
    public void EnablePlayerInput()
    {   
        DisableInput();
        inputActions.Player.Enable();
    }

    public void DisablePlayerInput()
    {
        inputActions.Player.Disable();
    }


}
