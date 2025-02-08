using System;
using GlobalConstants;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tools
{
    public class MovementManager : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private float rotationSpeed = 5f;
        
        private readonly InputAction _moveForwardByTransform = new("MoveForwardByTransform", InputActionType.Button, 
            $"{InputConstants.KeyBoard}/{InputConstants.Num8}");
        private readonly InputAction _moveLeftByTransform = new("MoveLeftByTransform", InputActionType.Button, 
            $"{InputConstants.KeyBoard}/{InputConstants.Num4}");
        private readonly InputAction _moveRightByTransform = new("MoveRightByTransform", InputActionType.Button, 
            $"{InputConstants.KeyBoard}/{InputConstants.Num6}");
        private readonly InputAction _moveBackwardByTransform = new("MoveBackwardByTransform", InputActionType.Button, 
            $"{InputConstants.KeyBoard}/{InputConstants.Num5}");

        private readonly InputAction _rotateLeftByTransform = new("RotateLeftByTransform", InputActionType.Button, 
            $"{InputConstants.KeyBoard}/{InputConstants.Num7}");
        private readonly InputAction _rotateRightByTransform = new("RotateRightByTransform", InputActionType.Button, 
            $"{InputConstants.KeyBoard}/{InputConstants.Num9}");
        
        private readonly InputAction _moveForwardByPhysics = new("MoveForwardByPhysics", InputActionType.Button, 
            $"{InputConstants.KeyBoard}/{InputConstants.W}");
        private readonly InputAction _moveLeftByPhysics = new("MoveLeftByPhysics", InputActionType.Button, 
            $"{InputConstants.KeyBoard}/{InputConstants.A}");
        private readonly InputAction _moveRightByPhysics = new("MoveRightByPhysics", InputActionType.Button, 
            $"{InputConstants.KeyBoard}/{InputConstants.D}");
        private readonly InputAction _moveBackwardByPhysics = new("MoveBackwardByPhysics", InputActionType.Button,
            $"{InputConstants.KeyBoard}/{InputConstants.S}");
        
        private readonly InputAction _rotateLeftByPhysics = new("RotateLeftByPhysics", InputActionType.Button, 
            $"{InputConstants.KeyBoard}/{InputConstants.Q}");
        private readonly InputAction _rotateRightByPhysics = new("RotateRightByPhysics", InputActionType.Button, 
            $"{InputConstants.KeyBoard}/{InputConstants.E}");

        private void Awake()
        {
            _moveForwardByTransform.performed += OnMoveForwardByTransform;
            _moveForwardByTransform.Enable();
            _moveLeftByTransform.performed += OnMoveLeftByTransform;
            _moveLeftByTransform.Enable();
            _moveRightByTransform.performed += OnMoveRightByTransform;
            _moveRightByTransform.Enable();
            _moveBackwardByTransform.performed += OnMoveBackwardByTransform;
            _moveBackwardByTransform.Enable();
            
            _rotateLeftByTransform.performed += OnRotateLeftByTransform;
            _rotateLeftByTransform.Enable();
            _rotateRightByTransform.performed += OnRotateRightByTransform;
            _rotateRightByTransform.Enable();
            
            _moveForwardByPhysics.performed += OnMoveForwardByPhysics;
            _moveForwardByPhysics.Enable();
            _moveLeftByPhysics.performed += OnMoveLeftByPhysics;
            _moveLeftByPhysics.Enable();
            _moveRightByPhysics.performed += OnMoveRightByPhysics;
            _moveRightByPhysics.Enable();
            _moveBackwardByPhysics.performed += OnMoveBackwardByPhysics;
            _moveBackwardByPhysics.Enable();
            
            _rotateLeftByPhysics.performed += OnRotateLeftByPhysics;
            _rotateLeftByPhysics.Enable();
            _rotateRightByPhysics.performed += OnRotateRightByPhysics;
            _rotateRightByPhysics.Enable();
        }

        private void OnMoveForwardByTransform(InputAction.CallbackContext context)
        {
            float z = transform.position.z;
            var delta = movementSpeed * Time.deltaTime;
            var newZ = z + delta;
            var position = transform.position;
            transform.position = new Vector3(position.x, position.y, newZ);
        }

        private void OnMoveLeftByTransform(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        private void OnMoveRightByTransform(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        private void OnMoveBackwardByTransform(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        private void OnRotateLeftByTransform(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        private void OnRotateRightByTransform(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        private void OnMoveForwardByPhysics(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        private void OnMoveLeftByPhysics(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        private void OnMoveRightByPhysics(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        private void OnMoveBackwardByPhysics(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        private void OnRotateLeftByPhysics(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        private void OnRotateRightByPhysics(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }
    }
}
