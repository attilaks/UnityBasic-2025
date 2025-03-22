using TMPro;
using Tools.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class UIManager : MonoBehaviour
	{
		[Header("Event invokers")] 
		[SerializeField] private WeaponController weaponController;
		
		[Header("Firearm data containers")]
		[SerializeField] private Image ammoImage;
		[SerializeField] private TextMeshProUGUI ammoCountText;
		[SerializeField] private TextMeshProUGUI ammoLeftText;
		
		[Header("Health data containers")]
		[SerializeField] private TextMeshProUGUI healthText;
		
		[Header("Settings references")]
		[SerializeField] private Scrollbar soundVolumeSlider;
		[SerializeField] private Button exitButton;
		[SerializeField] private GameObject settingsMenu;
		
		private bool _isMenuOpen;
		
		private void Awake()
		{
			weaponController.WeaponIsSwitched += OnWeaponIsSwitched;
			weaponController.CurrentWeaponAmmoCountChanged += OnCurrentWeaponAmmoCountChanged;
			
			exitButton.onClick.AddListener(OnExitButtonClicked);
		}

		private void OnDestroy()
		{
			weaponController.WeaponIsSwitched -= OnWeaponIsSwitched;
			weaponController.CurrentWeaponAmmoCountChanged -= OnCurrentWeaponAmmoCountChanged;
			
			exitButton.onClick.RemoveListener(OnExitButtonClicked);
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				ToggleMenu();
			}
		}

		private void ToggleMenu()
		{
			_isMenuOpen = !_isMenuOpen;
			
			if (_isMenuOpen)
			{
				PauseGame();
			}
			else
			{
				ResumeGame();
			}
			
			settingsMenu.SetActive(_isMenuOpen);
		}

		private void PauseGame()
		{
			Time.timeScale = 0f; 
			Cursor.lockState = CursorLockMode.None; 
			Cursor.visible = true;
		}

		private void ResumeGame()
		{
			Time.timeScale = 1f; 
			Cursor.lockState = CursorLockMode.Locked; 
			Cursor.visible = false;
		}

		private void OnWeaponIsSwitched(ushort currentAmmoCount, ushort clipCapacity, Sprite ammo)
		{
			ammoImage.sprite = ammo;
			ammoCountText.text = currentAmmoCount.ToString();
			ammoLeftText.text = clipCapacity.ToString();
		}
		
		private void OnCurrentWeaponAmmoCountChanged(ushort currentAmmoCount)
		{
			ammoCountText.text = currentAmmoCount.ToString();
		}
		
		private void OnExitButtonClicked()
		{
			Application.Quit();
			
			#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
			#endif
		}
	}
}