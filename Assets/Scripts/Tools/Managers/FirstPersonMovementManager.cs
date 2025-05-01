using System;
using GlobalConstants;
using SaveSystem;
using SaveSystem.Interfaces;
using Tools.Managers.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Tools.Managers
{
	[RequireComponent(typeof(AudioSource))]
	public class FirstPersonMovementManager : MonoBehaviour, IPlayerTransformReader
	{
		[Header("Movement settings")]
		[SerializeField] private float movementSpeed = 2f;
		
		[Header("Audio references")]
		[SerializeField] private AudioClip[] footstepClips;
		
		[Inject] private ISaveDataApplier _saveDataApplier;
		
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
		}

		private void Start()
		{
			var loadedSave = _saveDataApplier.GetSaveDataTobeApplied();
			if (loadedSave == null) return;
			
			transform.position = (Vector3)loadedSave.Value.playerPosition;
			transform.localRotation = Quaternion.Euler((Vector3)loadedSave.Value.playerRotation);
			Debug.Log("Player transform is loaded.");
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
		}

		private void OnDisable()
		{
			_moveForward.Disable();
			_moveLeft.Disable();
			_moveRight.Disable();
			_moveBack.Disable();
			
			_run.Disable();
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

		public SerializableVector3 PlayerPosition => transform.position;
		public SerializableVector3 PlayerRotation => transform.localRotation.eulerAngles;
	}
}