using System;
using Tools.Weapons.Firearms;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tools.Weapons
{
	public class WeaponController : MonoBehaviour
	{
		[SerializeField] private FireArm[] fireArms = Array.Empty<FireArm>();
		[SerializeField] private InputAction scrollAction;

		private int _currentWeaponIndex;

		private void Awake()
		{
			foreach (var gun in fireArms)
			{
				gun.SetActive(false);
			}
			
			fireArms[0].SetActive(true);
			scrollAction.Enable();
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
					SwitchWeapon(true);
					break;
				case < 0:
					SwitchWeapon(false);
					break;
			}
		}

		private void SwitchWeapon(bool next)
		{
			if (fireArms is not {Length: > 0}) return;
			
			var newIndex = next ? _currentWeaponIndex + 1 : _currentWeaponIndex - 1;

			if (newIndex < 0)
			{
				newIndex = fireArms.Length - 1;
			}
			else if (newIndex >= fireArms.Length)
			{
				newIndex = 0;
			}
			
			fireArms[_currentWeaponIndex].SetActive(false);
			fireArms[newIndex].SetActive(true);
			
			_currentWeaponIndex = newIndex;
		}
	}
}