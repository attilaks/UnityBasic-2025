using Tools.Managers.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using VContainer;
using Debug = UnityEngine.Debug;

namespace SaveSystem
{
	public sealed class SaveLoadManager : MonoBehaviour
	{
		[Header("Save/load actions")]
		[SerializeField] private InputAction saveGameAction;
		[SerializeField] private InputAction loadGameAction;
		
		[Inject] private ISaveService _saveService;
		[Inject] private IPlayerTransformReader _playerTransformReader;
		[Inject] private ICameraReader _cameraReader;
		
		private bool _quickSaveIsLoaded;
		
		private void Awake()
		{
			saveGameAction.performed += OnSaveGameActionPerformed;
			loadGameAction.performed += OnLoadGameActionPerformed;

			SceneManager.sceneLoaded += OnSceneLoaded;
		}

		private void OnDestroy()
		{
			saveGameAction.performed -= OnSaveGameActionPerformed;
			loadGameAction.performed -= OnLoadGameActionPerformed;
			
			SceneManager.sceneLoaded -= OnSceneLoaded;
		}

		private void OnEnable()
		{
			loadGameAction.Enable();
			saveGameAction.Enable();
		}

		private void OnDisable()
		{
			loadGameAction.Disable();
			saveGameAction.Disable();
		}
		
		private void OnLoadGameActionPerformed(InputAction.CallbackContext obj)
		{
			_quickSaveIsLoaded = true;
			SceneManager.LoadScene("Shooting");
		}

		private void OnSaveGameActionPerformed(InputAction.CallbackContext obj)
		{
			var saveData = new SaveData
			{
				playerPosition = _playerTransformReader.PlayerPosition,
				playerRotation = _playerTransformReader.PlayerRotation,
				cameraRotation = _cameraReader.CameraRotation,
				enemyPositions = null,
				currentAmmoCount = 0
			}; //todo
			_saveService.Save(saveData);
			Debug.Log("Saved game");
		}
		
		private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			if (!_quickSaveIsLoaded) return;
			
			var save = _saveService.Load();
			//todo
			
			_quickSaveIsLoaded = false;
			Debug.Log("Loaded game");
		}
	}
}