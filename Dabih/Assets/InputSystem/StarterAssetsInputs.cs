using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    public class StarterAssetsInputs : MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;
        public bool fire;
        public bool zoom;
        public bool weaponChange;
        public WeaponEnum weaponSelection;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
            {
                LookInput(value.Get<Vector2>());
            }
        }

        public void OnJump(InputValue value)
        {
            JumpInput(value.isPressed);
        }

        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }

        public void OnFire(InputValue value)
        {
            FireInput(value.isPressed);
        }

        public void OnZoom(InputValue value)
        {
            ZoomInput(value.isPressed);
        }

        public void OnRifleSelect(InputValue value)
        {
            WeaponSelectInput(value.isPressed, WeaponEnum.Rifle);
        }

        public void OnShotgunSelect(InputValue value)
        {
            WeaponSelectInput(value.isPressed, WeaponEnum.Shotgun);
        }
#endif


        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        private void FireInput(bool isPressed)
        {
            fire = isPressed;
        }

        private void ZoomInput(bool isPressed)
        {
            zoom = isPressed;
        }

        private void WeaponSelectInput(bool isPressed, WeaponEnum weapon)
        {
            weaponChange = isPressed;
            weaponSelection = weapon;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }

}