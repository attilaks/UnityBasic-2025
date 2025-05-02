using System;
using System.Linq;
using GlobalConstants;
using SaveSystem.Interfaces;
using ScriptableObjects.AssetMenus;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using Random = UnityEngine.Random;

namespace Tools.Weapons.Firearms
{
	[RequireComponent(typeof(AudioSource))]
	public class FireArm : MonoBehaviour
	{
		[SerializeField] protected WeaponData weaponData;
        
		[Header("Location references")]
		[SerializeField] private Transform casingExitLocation;
		[SerializeField] protected Transform firePoint;
		
		private AudioSource _audioSource;
		private Camera _firstPersonCamera;
		private Animator _animator;
		
		private const string AnimatorFire = "Fire";
		private static readonly int Fire = Animator.StringToHash(AnimatorFire);

		private const float DestroyTimer = 2f;
		private byte _currentAmmoCount;
		private float _nextFireTime;
		private bool _isInitialized;
		
		public event Action<byte> AmmoCountChanged = delegate { };

		public byte CurrentAmmoCount
		{
			get => _currentAmmoCount;
			protected set
			{
				var oldValue = _currentAmmoCount;
				if (value <= 0)
				{
					_currentAmmoCount = 0;
				}
				else if (value >= weaponData.ClipCapacity)
				{
					_currentAmmoCount = weaponData.ClipCapacity;
				}
				else
				{
					_currentAmmoCount = value;
				}

				if (oldValue != _currentAmmoCount)
				{
					AmmoCountChanged.Invoke(_currentAmmoCount);
				}
			}
		}

		public ushort AmmoLeft => weaponData.ClipCapacity;
		public Sprite AmmoUiSprite => weaponData.AmmoUiSprite;
		public byte FireArmId => weaponData.Id;
		
		private readonly InputAction _shootAction = new("Shoot", InputActionType.Button, 
			$"{InputConstants.Mouse}/{InputConstants.LeftButton}");

		private readonly InputAction _reloadAction = new("Reload", InputActionType.Button, 
			$"{InputConstants.KeyBoard}/{InputConstants.R}");

		public void Initialize(byte ammoCount)
		{
			if (_isInitialized) return;
			
			CurrentAmmoCount = ammoCount;
			_isInitialized = true;
		}

		protected void Awake()
		{
			_currentAmmoCount = weaponData.ClipCapacity;
			_audioSource = GetComponent<AudioSource>();
			_firstPersonCamera = Camera.main;

			if (gameObject.TryGetComponent<Animator>(out var animator))
			{
				_animator = animator;
				var animatorParameterNames = _animator.parameters.Select(x => x.name).ToArray();
				if (!animatorParameterNames.Contains(AnimatorFire))
				{
					throw new Exception($"Animator parameter {AnimatorFire} not found");
				}
			}

			if (weaponData.ShootSound == null)
				throw new Exception("Shoot sound could not be found");
			if (weaponData.ReloadSound == null)
				throw new Exception("Reload sound could not be found");
		}

		private void OnEnable()
		{
			_reloadAction.started += OnReloadActionPerformed;
			_shootAction.performed += OnShootActionPerformed;
			
			_shootAction.Enable();
			_reloadAction.Enable();
			_audioSource.enabled = true;
		}
		
		private void OnDisable()
		{
			_reloadAction.started -= OnReloadActionPerformed;
			_shootAction.performed -= OnShootActionPerformed;
			
			_shootAction.Disable();
			_reloadAction.Disable();
			_audioSource.enabled = false;
		}
		
		private void Update()
		{
			if (Time.timeScale == 0) return;
			if (weaponData.IsAutomatic && _shootAction.IsPressed())
			{
				Shoot();
			}
		}
		
		private void OnShootActionPerformed(InputAction.CallbackContext context)
		{
			if (Time.timeScale == 0) return;
			Shoot();
		}

		private void OnReloadActionPerformed(InputAction.CallbackContext obj)
		{
			if (Time.timeScale == 0) return;
			Reload();
		}

		private void Reload()
		{
			PlaySound(weaponData.ReloadSound);
			CurrentAmmoCount = weaponData.ClipCapacity;
		}

		private void Shoot()
		{
			if (_audioSource.isPlaying && _audioSource.clip == weaponData.ReloadSound)
			{
				return;
			}
			
			if (Time.time >= _nextFireTime)
			{
				if (CurrentAmmoCount > 0)
				{
					_nextFireTime = Time.time + weaponData.FireRate;
					if (_animator)
					{
						_animator.SetTrigger(Fire);
					}
					else
					{
						PullTheTrigger();
						CasingRelease();
					}
				}
				else
				{
					PlaySound(weaponData.EmptyClipSound);
				}
			}
		}
		
		protected virtual void PullTheTrigger()
		{
			SetMuzzleFlash();
			PlaySound(weaponData.ShootSound);

			if (weaponData.BulletPrefab)
			{
				SetFirePointDirection();
			
				var bulletRb = Instantiate(weaponData.BulletPrefab, firePoint.position, firePoint.rotation);
				var direction = Quaternion.Euler(
					Random.Range(-weaponData.BulletSpread, weaponData.BulletSpread),
					Random.Range(-weaponData.BulletSpread, weaponData.BulletSpread),
					0) * firePoint.forward;
				bulletRb.AddForce(direction * weaponData.BulletForce, ForceMode.Impulse);
			
				Destroy(bulletRb.gameObject, 2f);
			}
			
			--CurrentAmmoCount;
		}

		protected void PlaySound(AudioClip audioClip)
		{
			if (_audioSource.isPlaying)
			{
				_audioSource.Stop();
			}
			
			_audioSource.clip = audioClip;
			_audioSource.Play();
		}

		protected void SetMuzzleFlash()
		{
			if (weaponData.MuzzleFlashPrefab)
			{
				var tempFlash = Instantiate(weaponData.MuzzleFlashPrefab, firePoint.position, firePoint.rotation);
				Destroy(tempFlash, DestroyTimer);
			}
		}
		
		private void CasingRelease()
		{
			if (!casingExitLocation || !weaponData.CasingPrefab)
			{
				return;
			}

			var tempCasingRb = Instantiate(weaponData.CasingPrefab, casingExitLocation.position, casingExitLocation.rotation);
			tempCasingRb.AddExplosionForce(Random.Range(weaponData.EjectPower * 0.7f, weaponData.EjectPower), 
				casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f, 1f);
			tempCasingRb.AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);
			
			Destroy(tempCasingRb.gameObject, DestroyTimer);
		}

		protected void SetFirePointDirection()
		{
			Vector3 screenCenter = new Vector3((float)Screen.width / 2, (float)Screen.height / 2, 0);
			Ray ray = _firstPersonCamera.ScreenPointToRay(screenCenter);

			// Если луч пересекает какой-либо объект
			if (Physics.Raycast(ray, out var hit, weaponData.FireRange))
			{
				// Направляем объект на точку пересечения
				firePoint.transform.LookAt(hit.point);
			}
			else
			{
				// Если луч не пересекает объект, направляем объект на точку вдали
				Vector3 targetPosition = ray.GetPoint(weaponData.FireRange);
				firePoint.transform.LookAt(targetPosition);
			}
		}
	}
}