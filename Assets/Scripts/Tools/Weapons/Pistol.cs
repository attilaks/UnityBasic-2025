using System;
using System.Linq;
using GlobalConstants;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Tools.Weapons
{
	[RequireComponent(typeof(Animator))]
	public class Pistol : MonoBehaviour
	{
		[Header("Prefab References")]
		[SerializeField] private GameObject bulletPrefab;
		[SerializeField] private GameObject casingPrefab;
		[SerializeField] private GameObject muzzleFlashPrefab;
		
		[Header("Location References")]
		[SerializeField] private Transform casingExitLocation;
		[SerializeField] private Transform firePoint;
		
		[Header("Settings")]
		[Tooltip("Specify time to destroy the casing object")] [SerializeField] private float destroyTimer = 2f;
		[Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;
		
		[SerializeField] private float fireRate = 0.14f;
		[SerializeField] private float bulletForce;
		
		private float _nextFireTime;
		private Animator _animator;
		
		private const string AnimatorFire = "Fire";
		private static readonly int Fire = Animator.StringToHash(AnimatorFire);
		
		private readonly InputAction _shoot = new("Shoot", InputActionType.Button, 
			$"{InputConstants.Mouse}/{InputConstants.LeftButton}");

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
				Destroy(tempFlash, destroyTimer);
			}
			
			if (!bulletPrefab)
			{ return; }
			
			var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
			var rb = bullet.GetComponent<Rigidbody>();
			rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
			
			Destroy(bullet, 2f);
		}

		private void CasingRelease()
		{
			//Cancels function if ejection slot hasn't been set or there's no casing
			if (!casingExitLocation || !casingPrefab)
			{ return; }
			
			//Create the casing
			GameObject tempCasing;
			tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation);
			var tempCasingRigidBody = tempCasing.GetComponent<Rigidbody>();
			//Add force on casing to push it out
			tempCasingRigidBody.AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
			//Add torque to make casing spin in random direction
			tempCasingRigidBody.AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);
			
			//Destroy casing after X seconds
			Destroy(tempCasing, destroyTimer);
		}
	}
}