using System;
using GlobalConstants;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tools.Managers
{
	[RequireComponent(typeof(AudioSource))]
	public class FirstPersonMovementManager : MonoBehaviour
	{
		[Header("Save/load actions")]
		[SerializeField] private InputAction saveGameAction;
		[SerializeField] private InputAction loadGameAction;
		
		[Header("Movement settings")]
		[SerializeField] private float movementSpeed = 2f;
		
		[Header("Audio references")]
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
		
		private AudioSource _audioSource;
		private float _walkSoundSpeed;
		private float _runSoundSpeed;
		private bool _isRunning;
		private int _currentStepIndex;

		#region Monobehaviour methods

		private void Awake()
		{
			_audioSource = GetComponent<AudioSource>();
			_walkSoundSpeed = _audioSource.pitch;
			_runSoundSpeed = _walkSoundSpeed * 2f;

			saveGameAction.performed += OnSaveGameActionPerformed;
			loadGameAction.performed += OnLoadGameActionPerformed;
		}

		private void OnDestroy()
		{
			saveGameAction.performed -= OnSaveGameActionPerformed;
			loadGameAction.performed -= OnLoadGameActionPerformed;
		}

		private void Update()
		{
			if (Time.timeScale == 0) return;
			
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
		}
		
		private void OnEnable()
		{
			_moveForward.Enable();
			_moveLeft.Enable();
			_moveRight.Enable();
			_moveBack.Enable();
			
			_run.Enable();
			
			loadGameAction.Enable();
			saveGameAction.Enable();
		}

		private void OnDisable()
		{
			_moveForward.Disable();
			_moveLeft.Disable();
			_moveRight.Disable();
			_moveBack.Disable();
			
			_run.Disable();
			
			loadGameAction.Disable();
			saveGameAction.Disable();
		}

		#endregion

		private void Move(Vector3 direction)
		{
			var speed = _isRunning ? movementSpeed * 2 : movementSpeed;
			transform.Translate(direction * (speed * Time.deltaTime));
			PlayFootSteps();
		}

		private void PlayFootSteps()
		{
			if (_audioSource.isPlaying) return;
			
			_audioSource.clip = footstepClips[_currentStepIndex];
			_currentStepIndex = (_currentStepIndex + 1) % footstepClips.Length;
			_audioSource.pitch = _isRunning ? _runSoundSpeed : _walkSoundSpeed;
			
			_audioSource.Play();
		}
		
		private void OnLoadGameActionPerformed(InputAction.CallbackContext obj)
		{
			throw new NotImplementedException();
		}

		private void OnSaveGameActionPerformed(InputAction.CallbackContext obj)
		{
			throw new NotImplementedException();
		}
	}
}