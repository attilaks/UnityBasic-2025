using System;
using GlobalConstants;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace Tools
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovementManager : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 2f;
        [SerializeField] private float rotationSpeed = 2f;
        
        private Rigidbody _rigidbody;
        
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

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

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
                MoveForwardByTransform();
            }
            if (_moveLeftByTransform.IsPressed())
            {
                MoveLeftByTransform();
            }
            if (_moveRightByTransform.IsPressed())
            {
                MoveRightByTransform();
            }
            if (_moveBackByTransform.IsPressed())
            {
                MoveBackByTransform();
            }

            if (_rotateLeftByTransform.IsPressed())
            {
                RotateLeftByTransform();
            }
            if (_rotateRightByTransform.IsPressed())
            {
                RotateRightByTransform();
            }
            
            if (_moveForwardByPhysics.IsPressed())
            {
                MoveForwardByPhysics();
            }
            if (_moveLeftByPhysics.IsPressed())
            {
                MoveLeftByPhysics();
            }
            if (_moveRightByPhysics.IsPressed())
            {
                MoveRightByPhysics();
            }
            if (_moveBackByPhysics.IsPressed())
            {
                MoveBackByPhysics();
            }
            
            if (_rotateLeftByPhysics.IsPressed())
            {
                RotateLeftByPhysics();
            }
            if (_rotateRightByPhysics.IsPressed())
            {
                RotateRightByPhysics();
            }
        }

        #region ByTransform

        private void MoveForwardByTransform()
        {
            transform.Translate(Vector3.forward * (movementSpeed * Time.deltaTime));
        }
        
        private void MoveLeftByTransform()
        {
            transform.Translate(Vector3.left * (movementSpeed * Time.deltaTime));
        }
        
        private void MoveRightByTransform()
        {
            transform.Translate(Vector3.right * (movementSpeed * Time.deltaTime));
        }
        
        private void MoveBackByTransform()
        {
            transform.Translate(Vector3.back * (movementSpeed * Time.deltaTime));
        }
        
        private void RotateLeftByTransform()
        {
            var targetAngle = -90 * rotationSpeed * Time.deltaTime;
            transform.Rotate(0, targetAngle, 0);
        }
        
        private void RotateRightByTransform()
        {
            var targetAngle = 90 * rotationSpeed * Time.deltaTime;
            transform.Rotate(0, targetAngle, 0);
        }
        
        #endregion

        #region ByPhysics

        private void MoveForwardByPhysics()
        {
            _rigidbody.AddForce(Vector3.forward * (movementSpeed * Time.deltaTime), ForceMode.Impulse);
        }

        private void MoveLeftByPhysics()
        {
            _rigidbody.AddForce(Vector3.left * (movementSpeed * Time.deltaTime));
        }

        private void MoveRightByPhysics()
        {
            _rigidbody.AddForce(Vector3.right * (movementSpeed * Time.deltaTime));
        }

        private void MoveBackByPhysics()
        {
            _rigidbody.AddForce(Vector3.back * (movementSpeed * Time.deltaTime));
        }

        private void RotateLeftByPhysics()
        {
            var targetAngle = -90 * rotationSpeed * Time.deltaTime;
            _rigidbody.MoveRotation(Quaternion.Euler(0, targetAngle, 0));
        }

        private void RotateRightByPhysics()
        {
            var targetAngle = 90 * rotationSpeed * Time.deltaTime;
            _rigidbody.MoveRotation(Quaternion.Euler(0, targetAngle, 0));
        }

        #endregion
    }
}
