using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace SaveSystem
{
	public sealed class SaveLoadManager : MonoBehaviour
	{
		[Header("Save/load actions")]
		[SerializeField] private InputAction saveGameAction;
		[SerializeField] private InputAction loadGameAction;
		
		[Inject] private ISaveService _saveService;
		
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
			Debug.Log("Load Game Action");
		}

		private void OnSaveGameActionPerformed(InputAction.CallbackContext obj)
		{
			var saveData = new SaveData(); //todo
			_saveService.Save(saveData);
			Debug.Log("Save Game Action");
		}
	}
}