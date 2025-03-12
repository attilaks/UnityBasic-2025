using GlobalConstants;
using ScriptableObjects.AssetMenus;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tools.Weapons.Firearms
{
	public abstract class FireArm : MonoBehaviour
	{
		[SerializeField] protected Camera firstPersonCamera;
		[SerializeField] protected WeaponData weaponData;
        
		[Header("Location references")]
		[SerializeField] protected Transform casingExitLocation;
		[SerializeField] protected Transform firePoint;

		private const float DestroyTimer = 2f;
        
		protected float NextFireTime;
		
		protected readonly InputAction ShootAction = new("Shoot", InputActionType.Button, 
			$"{InputConstants.Mouse}/{InputConstants.LeftButton}");
		
		protected void OnEnable()
		{
			ShootAction.Enable();
		}
		
		protected void OnDisable()
		{
			ShootAction.Disable();
		}

		protected void Shoot()
		{
			if (Time.time >= NextFireTime)
			{
				NextFireTime = Time.time + weaponData.FireRate;
				PullTheTrigger();
				CasingRelease();
			}
		}
		
		protected void PullTheTrigger()
		{
			SetMuzzleFlash();

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