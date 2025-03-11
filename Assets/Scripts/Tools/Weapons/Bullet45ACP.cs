using Tools.Managers;
using UnityEngine;

namespace Tools.Weapons
{
    public class Bullet45Acp : MonoBehaviour
    {
        [SerializeField] private GameObject decalPrefab;
        [SerializeField] private float standardDamage = 10.0f;
        
        public float StandardDamage => standardDamage;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<EnemyHealthManager>(out var healthManager))
            {
                healthManager.Health -= standardDamage;
            }
            
            ContactPoint contact = other.contacts[0];
            Vector3 hitPosition = contact.point + contact.normal * 0.001f;
            Quaternion hitRotation = Quaternion.LookRotation(contact.normal);
            
            if (decalPrefab != null)
            {
                GameObject bulletHoleDecal = Instantiate(decalPrefab, hitPosition, hitRotation);
                bulletHoleDecal.transform.parent = other.transform;
                Destroy(bulletHoleDecal, 10f);
            }
            
            var ricochetChance = Random.Range(0, 2) == 1;
            Destroy(gameObject, ricochetChance ? 2f : 0f);
        }
    }
}
