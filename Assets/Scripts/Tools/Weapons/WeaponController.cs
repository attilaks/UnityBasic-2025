using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tools.Weapons
{
	public class WeaponController : MonoBehaviour
	{
		[SerializeField] private List<GameObject> fireArms = new(1);
		[SerializeField] private InputAction scrollAction;

		private int _currentWeaponIndex;
		private GameObject _currentFireArm;

		private void Awake()
		{
			scrollAction.Enable();
			
			if (fireArms is not {Count: > 0}) return;
			_currentFireArm = Instantiate(fireArms[0], transform);
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
			if (fireArms is not {Count: > 0}) return;
			
			var newIndex = next ? _currentWeaponIndex + 1 : _currentWeaponIndex - 1;

			if (newIndex < 0)
			{
				newIndex = fireArms.Count - 1;
			}
			else if (newIndex >= fireArms.Count)
			{
				newIndex = 0;
			}
			
			Destroy(_currentFireArm);
			_currentFireArm = Instantiate(fireArms[newIndex], transform);
			
			_currentWeaponIndex = newIndex;
		}
	}
}