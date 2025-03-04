using UnityEngine;

namespace Tools.Weapons
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private GameObject decalPrefab;

        private void OnCollisionEnter(Collision collision)
        {
            ContactPoint contact = collision.contacts[0];
            Vector3 hitPosition = contact.point;
            Quaternion hitRotation = Quaternion.LookRotation(contact.normal);

            // Создаем декаль в точке попадания
            if (decalPrefab != null)
            {
                GameObject decal = Instantiate(decalPrefab, hitPosition, hitRotation);
                decal.transform.parent = collision.transform; // Привязываем декаль к объекту
            }

            // Уничтожаем пулю после попадания
            Destroy(gameObject);
        }
    }
}
