using System;
using _Project.Input;
using UnityEngine.InputSystem;

namespace _Project.Runtime.Controllers
{
    public class PlayerInputController : IDisposable
    {
        public bool isShooting;
        public bool IsMoving;
        public bool IsWeaponChanged;
        public float Rotation;

        private int _weaponIndex;

        private bool _enabled;

        private readonly PlayerActions _playerActions = new PlayerActions();
        
        public int WeaponIndex => _weaponIndex;


        public void Enable()
        {
            if (_enabled) return;

            _playerActions.MainMap.Shoot.performed += OnShootHandler;
            _playerActions.MainMap.Shoot.canceled += OnShootHandler;
            _playerActions.MainMap.ForwardMovement.performed += OnMovementHandler;
            _playerActions.MainMap.ForwardMovement.canceled += OnMovementHandler;
            _playerActions.MainMap.Rotate.performed += OnRotatedHandler;
            _playerActions.MainMap.Rotate.canceled += OnRotatedHandler;
            _playerActions.MainMap.ChangeWeapon.performed += OnNumberKeyInputChangedHandler;
            _playerActions.MainMap.ChangeWeapon.canceled += OnNumberKeyInputChangedHandler;
            _playerActions.Enable();
            _enabled = true;
        }

        public void Disable()
        {
            _playerActions.MainMap.Shoot.performed -= OnShootHandler;
            _playerActions.MainMap.Shoot.canceled -= OnShootHandler;
            _playerActions.MainMap.ForwardMovement.performed -= OnMovementHandler;
            _playerActions.MainMap.ForwardMovement.canceled -= OnMovementHandler;
            _playerActions.MainMap.Rotate.performed -= OnRotatedHandler;
            _playerActions.MainMap.Rotate.canceled -= OnRotatedHandler;
            _playerActions.MainMap.ChangeWeapon.performed -= OnNumberKeyInputChangedHandler;
            _playerActions.MainMap.ChangeWeapon.canceled -= OnNumberKeyInputChangedHandler;
            _playerActions.MainMap.Disable();

            isShooting = false;
            IsMoving = false;
            _weaponIndex = 0;
            Rotation = 0;
            
            _enabled = false;
        }

        private void OnShootHandler(InputAction.CallbackContext ctx)
        {
            isShooting = ctx.performed;
        }

        private void OnMovementHandler(InputAction.CallbackContext ctx)
        {
            IsMoving = ctx.control.IsPressed();
        }

        private void OnRotatedHandler(InputAction.CallbackContext ctx)
        {
            Rotation = ctx.ReadValue<float>();
        }

        private void OnNumberKeyInputChangedHandler(InputAction.CallbackContext ctx)
        {
            IsWeaponChanged = ctx.control.IsPressed();
            if (int.TryParse(ctx.control.name, out var digit))
            {
                _weaponIndex = digit - 1;
            }
        }


        public void Dispose()
        {
            Disable();
        }
    }
}