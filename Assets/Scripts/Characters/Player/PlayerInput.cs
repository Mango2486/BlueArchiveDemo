using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Vector2 MoveDirection => inputActions.Player.Move.ReadValue<Vector2>();
    public bool IsMovePressed => MoveDirection != Vector2.zero;

    public bool IsAttacking { get; set; }

    public bool IsAiming { get; set; }


    private InputActions inputActions;

   
    private void OnEnable()
    {
        EnableInput();
    }

    private void Awake()
    {
        inputActions = new InputActions();
        AddListener();
    }

    private void OnDisable()
    {
        DisableInput();
    }

    private void OnDestroy()
    {
        RemoveListener();
    }

    #region 输入事件管理

    private void AddListener()
    {
        inputActions.Player.Aim.performed += OnAimPerformed;
        inputActions.Player.Aim.canceled += OnAimCanceled;
        inputActions.Player.Attack.performed += OnAttackPerformed;
        inputActions.Player.Attack.canceled += OnAttackCanceled;
    }

    private void RemoveListener()
    {
        inputActions.Player.Aim.performed -= OnAimPerformed;
        inputActions.Player.Aim.canceled -= OnAimCanceled;
        inputActions.Player.Attack.performed -= OnAttackPerformed;
        inputActions.Player.Attack.canceled -= OnAttackCanceled;
    }
    private void OnAttackCanceled(InputAction.CallbackContext obj)
    {
        IsAttacking = false;
    }

    private void OnAttackPerformed(InputAction.CallbackContext obj)
    {
        IsAttacking = true;
    }

    private void OnAimCanceled(InputAction.CallbackContext obj)
    {
        IsAiming = false;
    }

    private void OnAimPerformed(InputAction.CallbackContext obj)
    {
        IsAiming = true;
    }

    #endregion

    #region 动作表管理

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


    #endregion
   

}
