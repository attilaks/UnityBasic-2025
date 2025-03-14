using GlobalConstants;
using ScriptableObjects.AssetMenus;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Tools.Weapons.Firearms
{
	[RequireComponent(typeof(AudioSource))]
	public abstract class FireArm : MonoBehaviour
	{
		[SerializeField] protected Camera firstPersonCamera;
		[SerializeField] protected WeaponData weaponData;
        
		[Header("Location references")]
		[SerializeField] protected Transform casingExitLocation;
		[SerializeField] protected Transform firePoint;
		
		[Header("Audio references")]
		[SerializeField] private AudioClip shootSound;
		[SerializeField] private AudioClip reloadSound;
		
		private AudioSource _audioSource;

		private const float DestroyTimer = 2f;
		private byte _currentAmmoCount;
        
		protected float NextFireTime;

		protected byte CurrentAmmoCount
		{
			get => _currentAmmoCount;
			set
			{
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
			}
		}
		
		protected readonly InputAction ShootAction = new("Shoot", InputActionType.Button, 
			$"{InputConstants.Mouse}/{InputConstants.LeftButton}");

		private readonly InputAction _reloadAction = new("Reload", InputActionType.Button, 
			$"{InputConstants.KeyBoard}/{InputConstants.R}");

		protected void Awake()
		{
			CurrentAmmoCount = weaponData.ClipCapacity;
			_audioSource = GetComponent<AudioSource>();
		}

		protected void OnEnable()
		{
			_reloadAction.performed += OnReloadActionPerformed;
			
			ShootAction.Enable();
			_reloadAction.Enable();
		}
		
		protected void OnDisable()
		{
			_reloadAction.performed -= OnReloadActionPerformed;
			
			ShootAction.Disable();
			_reloadAction.Disable();
		}

		private void OnReloadActionPerformed(InputAction.CallbackContext obj)
		{
			Reload();
		}

		private void Reload()
		{
			CurrentAmmoCount = weaponData.ClipCapacity;
		}

		protected void Shoot()
		{
			if (Time.time >= NextFireTime && CurrentAmmoCount > 0)
			{
				NextFireTime = Time.time + weaponData.FireRate;
				PullTheTrigger();
				CasingRelease();
			}
		}
		
		protected void PullTheTrigger()
		{
			SetMuzzleFlash();
			PlaySound(shootSound);

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

		private void PlaySound(AudioClip audioClip)
		{
			if (audioClip)
			{
				_audioSource.PlayOneShot(audioClip);
			}
		}

		protected void SetMuzzleFlash()
		{
			if (weaponData.MuzzleFlashPrefab)
			{
				var tempFlash = Instantiate(weaponData.MuzzleFlashPrefab, firePoint.position, firePoint.rotation);
				Destroy(tempFlash, DestroyTimer);
			}
		}
		
		protected void CasingRelease()
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
			Ray ray = firstPersonCamera.ScreenPointToRay(screenCenter);

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

		public void SetActive(bool active)
		{
			gameObject.SetActive(active);
		}
	}
}