using System;
using System.Linq;
using GlobalConstants;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace Tools
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody))]
    public class MovementManager : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 2f;
        [SerializeField] private float rotationSpeed = 2f;
        
        private Rigidbody _rigidbody;
        private Animator _animator;
        
        private const string AnimatorSpeed = "Speed";
        private const string AnimatorDirectionX = "DirectionX";
        private const string AnimatorDirectionY = "DirectionY";
        
        private static readonly int Speed = Animator.StringToHash(AnimatorSpeed);
        private static readonly int DirectionX = Animator.StringToHash(AnimatorDirectionX);
        private static readonly int DirectionY = Animator.StringToHash(AnimatorDirectionY);
        private static readonly int WalkBack = Animator.StringToHash("WalkBack");

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

        protected void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
            
            var animatorParameterNames = _animator.parameters.Select(x => x.name).ToArray();
            if (!animatorParameterNames.Contains(AnimatorSpeed))
            {
                throw new Exception($"Animator parameter {AnimatorSpeed} not found");
            }

            if (!animatorParameterNames.Contains(AnimatorDirectionX))
            {
                throw new Exception($"Animator parameter {AnimatorDirectionX} not found");
            }

            if (!animatorParameterNames.Contains(AnimatorDirectionY))
            {
                throw new Exception($"Animator parameter {AnimatorDirectionY} not found");
            }
        }
        
        private void Update()
        {
            var directionX = 0f;
            var directionY = 0f;
            var speed = 0f;
            
            if (_moveForwardByTransform.IsPressed())
            {
                MoveForwardByTransform();
                speed = 1;
                directionY += 1;
            }
            if (_moveLeftByTransform.IsPressed())
            {
                MoveLeftByTransform();
                speed = 1;
                directionX -= 1;
            }
            if (_moveRightByTransform.IsPressed())
            {
                MoveRightByTransform();
                speed = 1;
                directionX += 1;
            }
            if (_moveBackByTransform.IsPressed())
            {
                MoveBackByTransform();
                speed = 1;
                directionY -= 1;
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
                speed = 1;
                directionY += 1;
            }
            if (_moveLeftByPhysics.IsPressed())
            {
                MoveLeftByPhysics();
                speed = 1;
                directionX -= 1;
            }
            if (_moveRightByPhysics.IsPressed())
            {
                MoveRightByPhysics();
                speed = 1;
                directionX += 1;
            }
            if (_moveBackByPhysics.IsPressed())
            {
                MoveBackByPhysics();
                speed = 1;
                directionY -= 1;
            }
            
            if (_rotateLeftByPhysics.IsPressed())
            {
                RotateLeftByPhysics();
            }
            if (_rotateRightByPhysics.IsPressed())
            {
                RotateRightByPhysics();
            }
            
            if (directionX == 0f && directionY == 0f)
                speed = 0;
            
            _animator.SetFloat(Speed, speed);
            _animator.SetFloat(DirectionX, directionX);
            _animator.SetFloat(DirectionY, directionY);
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
            _rigidbody.AddForce(Vector3.forward);
        }

        private void MoveLeftByPhysics()
        {
            _rigidbody.AddForce(Vector3.left);
        }

        private void MoveRightByPhysics()
        {
            _rigidbody.AddForce(Vector3.right);
        }

        private void MoveBackByPhysics()
        {
            _rigidbody.AddForce(Vector3.back);
        }

        private void RotateLeftByPhysics()
        {
            _rigidbody.AddTorque(-Vector3.up * (rotationSpeed * Time.fixedDeltaTime), ForceMode.VelocityChange);
        }

        private void RotateRightByPhysics()
        {
            _rigidbody.AddTorque(Vector3.up * (rotationSpeed * Time.fixedDeltaTime), ForceMode.VelocityChange);
        }

        #endregion
    }
}
