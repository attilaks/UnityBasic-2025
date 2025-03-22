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
		[SerializeField] private Button menuButton;
		[SerializeField] private Scrollbar soundVolumeScrollbar;
		[SerializeField] private Button exitButton;
		
		private void Awake()
		{
			weaponController.WeaponIsSwitched += OnWeaponIsSwitched;
			weaponController.CurrentWeaponAmmoCountChanged += OnCurrentWeaponAmmoCountChanged;
		}

		private void OnDestroy()
		{
			weaponController.WeaponIsSwitched -= OnWeaponIsSwitched;
			weaponController.CurrentWeaponAmmoCountChanged -= OnCurrentWeaponAmmoCountChanged;
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
	}
}