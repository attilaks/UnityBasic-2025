using System;
using System.Collections.Generic;
using System.Linq;
using SaveSystem.Interfaces;
using Tools.Weapons.Firearms;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Tools.Weapons
{
	public class WeaponController : MonoBehaviour, IWeaponReader
	{
		[SerializeField] private List<FireArm> fireArms;
		[SerializeField] private InputAction scrollAction;

		private int _currentWeaponIndex;
		private FireArm _currentFireArm;
		
		public event Action<ushort, ushort, Sprite> WeaponIsSwitched = delegate { };
		public event Action<ushort> CurrentWeaponAmmoCountChanged = delegate { };
		
		[Inject] private ISaveDataApplier _saveDataApplier;

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
		}

		private void Start()
		{
			if (fireArms is not {Count: > 0}) return;

			var currentFireArmId = fireArms.First().FireArmId;
			
			var loadedSave = _saveDataApplier.GetSaveDataTobeApplied();
			if (loadedSave != null) 
				currentFireArmId = loadedSave.Value.currentFireArmId;
			
			fireArms.ForEach(fireArm =>
			{
				var ammoInClip = loadedSave != null 
				                 && loadedSave.Value.GetAmmoDictionary().TryGetValue(fireArm.FireArmId, out var ammoCount) 
					? ammoCount : fireArm.CurrentAmmoCount;
				fireArm.Initialize(ammoInClip);
				if (fireArm.FireArmId == currentFireArmId) return;
				fireArm.gameObject.SetActive(false);
			});
			CurrentFireArm = fireArms.First(x => x.FireArmId == currentFireArmId);
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
		
		private void OnDestroy()
		{
			scrollAction.Disable();
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
		
		public byte CurrentFireArmId => _currentFireArm.FireArmId;
		public Dictionary<byte, byte> GetFireArmAmmoDict()
		{
			var dict = new Dictionary<byte, byte>(fireArms.Count);
			for (var i = 0; i < fireArms.Count; i++)
			{
				dict[fireArms[i].FireArmId] = fireArms[i].CurrentAmmoCount;
			}
			
			return dict;
		}
	}
}