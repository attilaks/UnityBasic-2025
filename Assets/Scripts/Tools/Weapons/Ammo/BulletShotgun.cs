using ScriptableObjects.AssetMenus;
using Tools.Managers;
using UnityEngine;

namespace Tools.Weapons.Ammo
{
	public class BulletShotgun : MonoBehaviour
	{
		[SerializeField] private AmmoData ammoData;
		
		private void OnCollisionEnter(Collision other)
		{
			if (other.gameObject.TryGetComponent<HealthManager>(out var healthManager))
			{
				healthManager.TakeDamage(ammoData.StandardDamage);
			}
            
			if (ammoData.DecalPrefab != null)
			{
				ContactPoint contact = other.contacts[0];
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