using System;
using System.Linq;
using GlobalConstants;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Tools.Weapons
{
	[RequireComponent(typeof(Animator))]
	public class Colt1911 : MonoBehaviour
	{
		[Header("Prefab references")]
		[SerializeField] private GameObject bulletPrefab;
		[SerializeField] private GameObject casingPrefab;
		[SerializeField] private GameObject muzzleFlashPrefab;
		
		[Header("Camera references")]
		[SerializeField] private Camera firstPersonCamera;
		
		[Header("Location references")]
		[SerializeField] private Transform casingExitLocation;
		[SerializeField] private Transform firePoint;
		
		[Header("Settings")]
		[Tooltip("Casing Ejection Speed")] 
		[SerializeField] private float ejectPower = 150f;
		[Tooltip("How fast the weapon can shoot")]
		[SerializeField] private float fireRate = 0.14f;
		[Tooltip("How far the weapon can shoot")] 
		[SerializeField] private float fireRange = 100f;
		[Tooltip("Force with which bullets fly out of weapon")]
		[SerializeField] private float bulletForce = 150;
		
		private float _nextFireTime;
		private Animator _animator;

		private const float DestroyTimer = 2f;

		private const string AnimatorFire = "Fire";
		private static readonly int Fire = Animator.StringToHash(AnimatorFire);
		
		private readonly InputAction _shoot = new("Shoot", InputActionType.Button, 
			$"{InputConstants.Mouse}/{InputConstants.LeftButton}");

		#region Monobehaviour methods

		private void Awake()
		{
			_animator = gameObject.GetComponent<Animator>();
			
			var animatorParameterNames = _animator.parameters.Select(x => x.name).ToArray();
			if (!animatorParameterNames.Contains(AnimatorFire))
			{
				throw new Exception($"Animator parameter {AnimatorFire} not found");
			}
		}

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

		#endregion
		
		private void OnShootPerformed(InputAction.CallbackContext context)
		{
			if (Time.time >= _nextFireTime)
			{
				_animator.SetTrigger(Fire);
				_nextFireTime = Time.time + fireRate;
			}
		}

		private void Shoot()
		{
			if (muzzleFlashPrefab)
			{
				var tempFlash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
				Destroy(tempFlash, DestroyTimer);
			}
			
			if (!bulletPrefab)
			{ return; }
			
			SetFirePointDirection();
			
			var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
			var rb = bullet.GetComponent<Rigidbody>();
			rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
			
			Destroy(bullet, 2f);
		}

		private void CasingRelease()
		{
			if (!casingExitLocation || !casingPrefab)
			{
				return;
			}

			var tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation);
			var tempCasingRigidBody = tempCasing.GetComponent<Rigidbody>();
			tempCasingRigidBody.AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), 
				(casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
			tempCasingRigidBody.AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);
			
			Destroy(tempCasing, DestroyTimer);
		}

		private void SetFirePointDirection()
		{
			Vector3 screenCenter = new Vector3((float)Screen.width / 2, (float)Screen.height / 2, 0);
			Ray ray = firstPersonCamera.ScreenPointToRay(screenCenter);

			// Если луч пересекает какой-либо объект
			if (Physics.Raycast(ray, out var hit, fireRange))
			{
				// Направляем объект на точку пересечения
				firePoint.transform.LookAt(hit.point);
			}
			else
			{
				// Если луч не пересекает объект, направляем объект на точку вдали
				Vector3 targetPosition = ray.GetPoint(fireRange);
				firePoint.transform.LookAt(targetPosition);
			}
		}
	}
}