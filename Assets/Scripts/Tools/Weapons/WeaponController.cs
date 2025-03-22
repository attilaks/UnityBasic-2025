using System;
using System.Collections.Generic;
using Tools.Weapons.Firearms;
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

		private FireArm CurrentFireArm
		{
			get => _currentFireArm;
			set
			{
				_currentFireArm?.gameObject.SetActive(false);
				_currentFireArm = value;
				_currentFireArm.gameObject.SetActive(true);
				WeaponIsSwitched.Invoke(_currentFireArm.CurrentAmmoCount, _currentFireArm.AmmoLeft, _currentFireArm.AmmoUiSprite);
			}
		}
		
		public event Action<ushort, ushort, Sprite> WeaponIsSwitched = delegate { };

		private void Start()
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
	}
}