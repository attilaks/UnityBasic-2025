using GlobalConstants;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tools.Managers
{
	[RequireComponent(typeof(AudioSource))]
	public class FirstPersonMovementManager : MonoBehaviour
	{
		[Header("Movement settings")]
		[SerializeField] private float movementSpeed = 2f;
		[SerializeField] private float rotationSpeed = 2f;
		
		[Header("Audio settings")]
		[SerializeField] private AudioClip[] footstepClips;
		
		private readonly InputAction _moveForward = new("MoveForward", InputActionType.Value, 
			$"{InputConstants.KeyBoard}/{InputConstants.W}");
		private readonly InputAction _moveLeft = new("MoveLeft", InputActionType.Value, 
			$"{InputConstants.KeyBoard}/{InputConstants.A}");
		private readonly InputAction _moveRight = new("MoveRight", InputActionType.Value, 
			$"{InputConstants.KeyBoard}/{InputConstants.D}");
		private readonly InputAction _moveBack = new("MoveBackward", InputActionType.Value, 
			$"{InputConstants.KeyBoard}/{InputConstants.S}");
		
		private readonly InputAction _run = new("Run", InputActionType.Value, 
			$"{InputConstants.KeyBoard}/{InputConstants.LeftShift}");

		private readonly InputAction _rotateLeft = new("RotateLeft", InputActionType.Value, 
			$"{InputConstants.KeyBoard}/{InputConstants.Q}");
		private readonly InputAction _rotateRight = new("RotateRight", InputActionType.Value, 
			$"{InputConstants.KeyBoard}/{InputConstants.E}");
		
		private AudioSource _footStepsAudioSource;
		private float _walkSoundSpeed;
		private float _runSoundSpeed;
		private bool _isRunning;
		private int _currentStepIndex;

		#region Monobehaviour methods

		private void Awake()
		{
			_footStepsAudioSource = GetComponent<AudioSource>();
			_walkSoundSpeed = _footStepsAudioSource.pitch;
			_runSoundSpeed = _walkSoundSpeed * 2f;
		}

		private void Update()
		{
			_isRunning = _run.IsPressed();
			
			if (_moveForward.IsPressed())
			{
				Move(Vector3.forward);
			}
			if (_moveLeft.IsPressed())
			{
				Move(Vector3.left);
			}
			if (_moveRight.IsPressed())
			{
				Move(Vector3.right);
			}
			if (_moveBack.IsPressed())
			{
				Move(Vector3.back);
			}

			if (_rotateLeft.IsPressed())
			{
				Rotate(-90f);
			}
			if (_rotateRight.IsPressed())
			{
				Rotate(90f);
			}
		}
		
		private void OnEnable()
		{
			_moveForward.Enable();
			_moveLeft.Enable();
			_moveRight.Enable();
			_moveBack.Enable();
			
			_run.Enable();
            
			_rotateLeft.Enable();
			_rotateRight.Enable();
		}

		private void OnDisable()
		{
			_moveForward.Disable();
			_moveLeft.Disable();
			_moveRight.Disable();
			_moveBack.Disable();
			
			_run.Disable();
            
			_rotateLeft.Disable();
			_rotateRight.Disable();
		}

		#endregion

		private void Move(Vector3 direction)
		{
			var speed = _isRunning ? movementSpeed * 2 : movementSpeed;
			transform.Translate(direction * (speed * Time.deltaTime));
			PlayFootSteps();
		}
		
		private void Rotate(float angle)
		{
			var targetAngle = angle * rotationSpeed * Time.deltaTime;
			transform.Rotate(0, targetAngle, 0);
		}

		private void PlayFootSteps()
		{
			if (_footStepsAudioSource.isPlaying) return;
			
			_footStepsAudioSource.clip = footstepClips[_currentStepIndex];
			_currentStepIndex = (_currentStepIndex + 1) % footstepClips.Length;
			_footStepsAudioSource.pitch = _isRunning ? _runSoundSpeed : _walkSoundSpeed;
			
			_footStepsAudioSource.Play();
		}
	}
}