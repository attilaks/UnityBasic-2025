using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tools.Weapons
{
	public class WeaponController : MonoBehaviour
	{
		[SerializeField] private List<FireArm> fireArms;
		[SerializeField] private InputAction scrollAction;

		private int _currentWeaponIndex;
		private FireArm _currentFireArm;
		
		public event Action<ushort, ushort, Sprite> WeaponIsSwitched = delegate { };
		public event Action<ushort> CurrentWeaponAmmoCountChanged = delegate { };

		private FireArm CurrentFireArm
		{
			set
			{
				if (_currentFireArm)
				{
					_currentFireArm.AmmoCountChanged -= OnCurrentWeaponAmmoCountChanged;
					_currentFireArm.gameObject.SetActive(false);
				}
				
				_currentFireArm = value;
				_currentFireArm.AmmoCountChanged += OnCurrentWeaponAmmoCountChanged;
				_currentFireArm.gameObject.SetActive(true);
				WeaponIsSwitched.Invoke(_currentFireArm.CurrentAmmoCount, _currentFireArm.AmmoLeft, _currentFireArm.AmmoUiSprite);
			}
		}

		private void Awake()
		{
			scrollAction.Enable();
			
			if (fireArms is not {Count: > 0}) return;
			CurrentFireArm = fireArms[0];

			for (int i = 1; i < fireArms.Count; i++)
			{
				fireArms[i].gameObject.SetActive(false);
			}
		}

		private void OnDestroy()
		{
			scrollAction.Disable();
		}

		private void Update()
		{
			if (Time.timeScale == 0) return;
			
			Vector2 scrollValue = scrollAction.ReadValue<Vector2>();

			switch (scrollValue.y)
			{
				case > 0:
					SwitchWeapon(_currentWeaponIndex + 1);
					break;
				case < 0:
					SwitchWeapon(_currentWeaponIndex - 1);
					break;
			}
		}

		private void SwitchWeapon(int newIndex)
		{
			if (fireArms is not {Count: > 0}) return;
		
			_currentWeaponIndex = newIndex < 0 ? fireArms.Count - 1 : newIndex >= fireArms.Count ? 0 : newIndex;
			CurrentFireArm = fireArms[_currentWeaponIndex];
		}
		
		private void OnCurrentWeaponAmmoCountChanged(byte ammoCount)
		{
			CurrentWeaponAmmoCountChanged.Invoke(ammoCount);
		}
	}
}