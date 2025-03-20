using System.Collections;
using UnityEngine;

namespace Tools.Weapons.Firearms
{
	public sealed class Shotgun : FireArm
	{
		private const byte BlastCount = 6;

		protected override void PullTheTrigger()
		{
			SetMuzzleFlash();
			PlaySound(weaponData.ShootSound);

			if (weaponData.BulletPrefab)
			{
				SetFirePointDirection();
				StartCoroutine(SpreadBullets());
			}
			
			--CurrentAmmoCount;
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