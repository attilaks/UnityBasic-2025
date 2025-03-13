using Characters;
using UnityEngine;

namespace Tools
{
    public class FirstAidKit : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player>(out var player))
            {
                player.PickUpFirstAidKit();
                Destroy(gameObject);
            }
        }
    }
}
