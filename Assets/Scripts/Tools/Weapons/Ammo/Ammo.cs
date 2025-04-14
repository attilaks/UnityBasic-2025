using ScriptableObjects.AssetMenus;
using Tools.Managers;
using UnityEngine;

namespace Tools.Weapons.Ammo
{
	public class Ammo : MonoBehaviour
	{
		[SerializeField] private AmmoData ammoData;
		[SerializeField] private ParticleSystem sparksEffect;
		
		private void OnCollisionEnter(Collision other)
		{
			ContactPoint contact = other.GetContact(0);
			
			if (other.gameObject.TryGetComponent<HealthManager>(out var healthManager))
			{
				healthManager.TakeDamage(ammoData.StandardDamage);
				if (sparksEffect)
				{
					var sparks = Instantiate(sparksEffect, contact.point, Quaternion.LookRotation(contact.normal));
					sparks.Play();
				}
			}
            
			if (ammoData.DecalPrefab)
			{
				Vector3 hitPosition = contact.point + contact.normal * 0.001f;
				Quaternion hitRotation = Quaternion.LookRotation(contact.normal);
                
				GameObject bulletHoleDecal = Instantiate(ammoData.DecalPrefab, hitPosition, hitRotation);
				bulletHoleDecal.transform.parent = other.transform;
				Destroy(bulletHoleDecal, 10f);
			}
            
			var ricochetChance = Random.Range(0, 2) == 1;
			Destroy(gameObject, ricochetChance ? 2f : 0f);
		}
	}
}