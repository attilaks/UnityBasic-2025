using System.Collections;
using GlobalConstants;
using ScriptableObjects.AssetMenus;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tools.Weapons.Firearms
{
	public class Shotgun : MonoBehaviour
	{
		[SerializeField] private Camera firstPersonCamera;
		[SerializeField] private WeaponData weaponData;
        
		[Header("Location references")]
		[SerializeField] private Transform casingExitLocation;
		[SerializeField] private Transform firePoint;
        
		private float _nextFireTime;
		
		private const byte BlastCount = 6;
		private const float DestroyTimer = 2f;
        
		private readonly InputAction _shoot = new("Shoot", InputActionType.Button, 
			$"{InputConstants.Mouse}/{InputConstants.LeftButton}");
		
		private void OnEnable()
		{
			_shoot.performed += OnShootPerformed;
			_shoot.Enable();
		}

		private void OnDisable()
		{
			_shoot.performed -= OnShootPerformed;
			_shoot.Disable();
		}
		
		private void OnShootPerformed(InputAction.CallbackContext context)
		{
			if (Time.time >= _nextFireTime)
			{
				_nextFireTime = Time.time + weaponData.FireRate;
				Shoot();
				CasingRelease();
			}
		}

		private void Shoot()
		{
			if (weaponData.MuzzleFlashPrefab)
			{
				var tempFlash = Instantiate(weaponData.MuzzleFlashPrefab, firePoint.position, firePoint.rotation);
				Destroy(tempFlash, DestroyTimer);
			}

			if (weaponData.BulletPrefab)
			{
				SetFirePointDirection();

				StartCoroutine(SpreadBullets());

				// for (var i = 0; i < BlastCount; i++)
				// {
				// 	var spreadX = Random.Range(-weaponData.BulletSpread, weaponData.BulletSpread);
				// 	var spreadY = Random.Range(-weaponData.BulletSpread, weaponData.BulletSpread);
				// 	var direction = Quaternion.Euler(spreadX, spreadY, 0) * firePoint.forward;
				// 	
				// 	var bullet = Instantiate(weaponData.BulletPrefab, firePoint.position, firePoint.rotation);
				// 	var rb = bullet.GetComponent<Rigidbody>();
				// 	rb.AddForce(direction * weaponData.BulletForce, ForceMode.Impulse);
				//
				// 	Destroy(bullet, 2f);
				// }

				// var spreadX = Random.Range(-weaponData.BulletSpread, weaponData.BulletSpread);
				// var spreadY = Random.Range(-weaponData.BulletSpread, weaponData.BulletSpread);
				// var direction = Quaternion.Euler(spreadX, spreadY, 0) * firePoint.forward;
				// 	
				// var bullet = Instantiate(weaponData.BulletPrefab, firePoint.position, firePoint.rotation);
				// var rb = bullet.GetComponent<Rigidbody>();
				// rb.AddForce(direction * weaponData.BulletForce, ForceMode.Impulse);
				//
				// Destroy(bullet, 2f);
			}
		}

		private IEnumerator SpreadBullets()
		{
			for (var blastsLeft = BlastCount; blastsLeft > 0; --blastsLeft)
			{
				var spreadX = Random.Range(-weaponData.BulletSpread, weaponData.BulletSpread);
				var spreadY = Random.Range(-weaponData.BulletSpread, weaponData.BulletSpread);
				var direction = Quaternion.Euler(spreadX, spreadY, 0) * firePoint.forward;
					
				var bullet = Instantiate(weaponData.BulletPrefab, firePoint.position, firePoint.rotation);
				var rb = bullet.GetComponent<Rigidbody>();
				rb.AddForce(direction * weaponData.BulletForce, ForceMode.Impulse);

				Destroy(bullet, 2f);
				yield return new WaitForSeconds(0.015f);
			}
		}

		private void CasingRelease()
		{
			if (!casingExitLocation || !weaponData.CasingPrefab)
			{
				return;
			}

			var tempCasing = Instantiate(weaponData.CasingPrefab, casingExitLocation.position, casingExitLocation.rotation);
			var tempCasingRigidBody = tempCasing.GetComponent<Rigidbody>();
			tempCasingRigidBody.AddExplosionForce(Random.Range(weaponData.EjectPower * 0.7f, weaponData.EjectPower), 
				casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f, 1f);
			tempCasingRigidBody.AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);
			
			Destroy(tempCasing, DestroyTimer);
		}

		private void SetFirePointDirection()
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
	}
}