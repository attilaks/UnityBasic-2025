using System;
using GlobalConstants;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tools
{
	public class FirstPersonMovementManager : MonoBehaviour
	{
		[SerializeField] private float movementSpeed = 2f;
		[SerializeField] private float rotationSpeed = 2f;
		
		private readonly InputAction _moveForward = new("MoveForward", InputActionType.Value, 
			$"{InputConstants.KeyBoard}/{InputConstants.W}");
		private readonly InputAction _moveLeft = new("MoveLeft", InputActionType.Value, 
			$"{InputConstants.KeyBoard}/{InputConstants.A}");
		private readonly InputAction _moveRight = new("MoveRight", InputActionType.Value, 
			$"{InputConstants.KeyBoard}/{InputConstants.D}");
		private readonly InputAction _moveBack = new("MoveBackward", InputActionType.Value, 
			$"{InputConstants.KeyBoard}/{InputConstants.S}");

		private readonly InputAction _rotateLeft = new("RotateLeft", InputActionType.Value, 
			$"{InputConstants.KeyBoard}/{InputConstants.Q}");
		private readonly InputAction _rotateRight = new("RotateRight", InputActionType.Value, 
			$"{InputConstants.KeyBoard}/{InputConstants.E}");

		#region Monobehaviour methods

		private void Update()
		{
			if (_moveForward.IsPressed())
			{
				MoveForwardByTransform();
			}
			if (_moveLeft.IsPressed())
			{
				MoveLeftByTransform();
			}
			if (_moveRight.IsPressed())
			{
				MoveRightByTransform();
			}
			if (_moveBack.IsPressed())
			{
				MoveBackByTransform();
			}

			if (_rotateLeft.IsPressed())
			{
				RotateLeftByTransform();
			}
			if (_rotateRight.IsPressed())
			{
				RotateRightByTransform();
			}
		}
		
		private void OnEnable()
		{
			_moveForward.Enable();
			_moveLeft.Enable();
			_moveRight.Enable();
			_moveBack.Enable();
            
			_rotateLeft.Enable();
			_rotateRight.Enable();
		}

		private void OnDisable()
		{
			_moveForward.Disable();
			_moveLeft.Disable();
			_moveRight.Disable();
			_moveBack.Disable();
            
			_rotateLeft.Disable();
			_rotateRight.Disable();
		}

		#endregion
		
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
	}
}