using UnityEngine;

namespace Tools.Weapons
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private GameObject decalPrefab;
        
        private GameObject decal;

        private void OnCollisionEnter(Collision collision)
        {
            ContactPoint contact = collision.contacts[0];
            Vector3 hitPosition = contact.point + contact.normal * 0.001f;
            Quaternion hitRotation = Quaternion.LookRotation(contact.normal);
            
            if (decalPrefab != null)
            {
                GameObject bulletHoleDecal = Instantiate(decalPrefab, hitPosition, hitRotation);
                bulletHoleDecal.transform.parent = collision.transform;
                Destroy(bulletHoleDecal, 10f);
            }
            
            var ricochetChance = Random.Range(0, 2) == 1;
            Destroy(gameObject, ricochetChance ? 2f : 0f);
        }
    }
}
