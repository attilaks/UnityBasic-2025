using System.Collections;
using SaveSystem.Interfaces;
using Tools.Managers.Interfaces;
using Tools.Weapons;
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
		[Inject] private IWeaponReader _weaponReader;
		
		private void Awake()
		{
			saveGameAction.performed += OnSaveGameActionPerformed;
			loadGameAction.performed += OnLoadGameActionPerformed;
		}

		private void OnDestroy()
		{
			saveGameAction.performed -= OnSaveGameActionPerformed;
			loadGameAction.performed -= OnLoadGameActionPerformed;
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
			_saveService.Load();
			StartCoroutine(LoadScene());
		}
		
		private void OnSaveGameActionPerformed(InputAction.CallbackContext obj)
		{
			var saveData = new SaveData
			{
				playerPosition = _playerTransformReader.PlayerPosition,
				playerRotation = _playerTransformReader.PlayerRotation,
				cameraRotation = _cameraReader.CameraRotation,
				currentFireArmId = _weaponReader.CurrentFireArmId,
				// FireArmAmmoCountDict = _weaponReader.GetFireArmAmmoDict()
			}; //todo
			saveData.SetAmmoDictionary(_weaponReader.GetFireArmAmmoDict());
			_saveService.Save(saveData);
			Debug.Log("Saved game");
		}
		
		private IEnumerator LoadScene()
		{
			yield return SceneManager.LoadSceneAsync("LoadingScene");
		}
	}
}