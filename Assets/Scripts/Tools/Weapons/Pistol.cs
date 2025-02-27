using System;
using GlobalConstants;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tools.Weapons
{
	public class Pistol : MonoBehaviour
	{
		[SerializeField] private GameObject bulletPrefab;
		[SerializeField] private Transform firePoint;
		[SerializeField] private float fireRate;
		[SerializeField] private float bulletForce;
		
		private float _nextFireTime;
		
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
				Shoot();
				_nextFireTime = Time.time + fireRate;
			}
		}

		private void Shoot()
		{
			GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
			Rigidbody rb = bullet.GetComponent<Rigidbody>();
			
			rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
			
			Destroy(bullet, 2f);
		}
	}
}