using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tools.Weapons.Firearms
{
	public sealed class Shotgun : FireArm
	{
		private const byte BlastCount = 6;
		
		private new void OnEnable()
		{
			ShootAction.performed += OnShootActionPerformed;
			base.OnEnable();
		}

		private new void OnDisable()
		{
			ShootAction.performed -= OnShootActionPerformed;
			base.OnDisable();
		}
		
		private void OnShootActionPerformed(InputAction.CallbackContext context)
		{
			Shoot();
		}
		
		private new void Shoot()
		{
			if (Time.time >= NextFireTime)
			{
				NextFireTime = Time.time + weaponData.FireRate;
				PullTheTrigger();
				CasingRelease();
			}
		}

		private new void PullTheTrigger()
		{
			SetMuzzleFlash();

			if (weaponData.BulletPrefab)
			{
				SetFirePointDirection();

				StartCoroutine(SpreadBullets());
			}
		}

		private IEnumerator SpreadBullets()
		{
			for (var blastsLeft = BlastCount; blastsLeft > 0; --blastsLeft)
			{
				var spreadX = Random.Range(-weaponData.BulletSpread, weaponData.BulletSpread);
				var spreadY = Random.Range(-weaponData.BulletSpread, weaponData.BulletSpread);
				var direction = Quaternion.Euler(spreadX, spreadY, 0) * firePoint.forward;
					
				var bulletRb = Instantiate(weaponData.BulletPrefab, firePoint.position, firePoint.rotation);
				bulletRb.AddForce(direction * weaponData.BulletForce, ForceMode.Impulse);

				Destroy(bulletRb.gameObject, 2f);
				yield return new WaitForSeconds(0.015f);
			}
		}
	}
}