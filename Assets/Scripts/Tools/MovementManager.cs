using System;
using GlobalConstants;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace Tools
{
    public class MovementManager : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 2f;
        [SerializeField] private float rotationSpeed = 2f;
        
        private readonly InputAction _moveForwardByTransform = new("MoveForwardByTransform", InputActionType.Value, 
            $"{InputConstants.KeyBoard}/{InputConstants.Num8}");
        private readonly InputAction _moveLeftByTransform = new("MoveLeftByTransform", InputActionType.Value, 
            $"{InputConstants.KeyBoard}/{InputConstants.Num4}");
        private readonly InputAction _moveRightByTransform = new("MoveRightByTransform", InputActionType.Value, 
            $"{InputConstants.KeyBoard}/{InputConstants.Num6}");
        private readonly InputAction _moveBackByTransform = new("MoveBackwardByTransform", InputActionType.Value, 
            $"{InputConstants.KeyBoard}/{InputConstants.Num5}");

        private readonly InputAction _rotateLeftByTransform = new("RotateLeftByTransform", InputActionType.Value, 
            $"{InputConstants.KeyBoard}/{InputConstants.Num7}");
        private readonly InputAction _rotateRightByTransform = new("RotateRightByTransform", InputActionType.Value, 
            $"{InputConstants.KeyBoard}/{InputConstants.Num9}");
        
        private readonly InputAction _moveForwardByPhysics = new("MoveForwardByPhysics", InputActionType.Value, 
            $"{InputConstants.KeyBoard}/{InputConstants.W}");
        private readonly InputAction _moveLeftByPhysics = new("MoveLeftByPhysics", InputActionType.Value, 
            $"{InputConstants.KeyBoard}/{InputConstants.A}");
        private readonly InputAction _moveRightByPhysics = new("MoveRightByPhysics", InputActionType.Value, 
            $"{InputConstants.KeyBoard}/{InputConstants.D}");
        private readonly InputAction _moveBackByPhysics = new("MoveBackwardByPhysics", InputActionType.Value,
            $"{InputConstants.KeyBoard}/{InputConstants.S}");
        
        private readonly InputAction _rotateLeftByPhysics = new("RotateLeftByPhysics", InputActionType.Value, 
            $"{InputConstants.KeyBoard}/{InputConstants.Q}");
        private readonly InputAction _rotateRightByPhysics = new("RotateRightByPhysics", InputActionType.Value, 
            $"{InputConstants.KeyBoard}/{InputConstants.E}");

        #region Monobehaviour methods

        private void OnEnable()
        {
            _moveForwardByTransform.Enable();
            _moveLeftByTransform.Enable();
            _moveRightByTransform.Enable();
            _moveBackByTransform.Enable();
            
            _rotateLeftByTransform.Enable();
            _rotateRightByTransform.Enable();
            
            _moveForwardByPhysics.Enable();
            _moveLeftByPhysics.Enable();
            _moveRightByPhysics.Enable();
            _moveBackByPhysics.Enable();
            
            _rotateLeftByPhysics.Enable();
            _rotateRightByPhysics.Enable();
        }

        private void OnDisable()
        {
            _moveForwardByTransform.Disable();
            _moveLeftByTransform.Disable();
            _moveRightByTransform.Disable();
            _moveBackByTransform.Disable();
            
            _rotateLeftByTransform.Disable();
            _rotateRightByTransform.Disable();
            
            _moveForwardByPhysics.Disable();
            _moveLeftByPhysics.Disable();
            _moveRightByPhysics.Disable();
            _moveBackByPhysics.Disable();
            
            _rotateLeftByPhysics.Disable();
            _rotateRightByPhysics.Disable();
        }

        #endregion

        private void Update()
        {
            if (_moveForwardByTransform.IsPressed())
            {
                MoveForward();
            }
            if (_moveLeftByTransform.IsPressed())
            {
                MoveLeft();
            }
            if (_moveRightByTransform.IsPressed())
            {
                MoveRight();
            }
            if (_moveBackByTransform.IsPressed())
            {
                MoveBack();
            }

            if (_rotateLeftByTransform.IsPressed())
            {
                RotateLeft();
            }
            if (_rotateRightByTransform.IsPressed())
            {
                RotateRight();
            }
        }

        private void MoveForward()
        {
            transform.Translate(Vector3.forward * (movementSpeed * Time.deltaTime));
        }
        
        private void MoveLeft()
        {
            transform.Translate(Vector3.left * (movementSpeed * Time.deltaTime));
        }
        
        private void MoveRight()
        {
            transform.Translate(Vector3.right * (movementSpeed * Time.deltaTime));
        }
        
        private void MoveBack()
        {
            transform.Translate(Vector3.back * (movementSpeed * Time.deltaTime));
        }
        
        private void RotateLeft()
        {
            var targetAngle = -90 * rotationSpeed * Time.deltaTime;
            transform.Rotate(0, targetAngle, 0);
        }
        
        private void RotateRight()
        {
            var targetAngle = 90 * rotationSpeed * Time.deltaTime;
            transform.Rotate(0, targetAngle, 0);
        }
    }
}
