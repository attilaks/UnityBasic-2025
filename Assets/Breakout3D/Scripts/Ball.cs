using UnityEngine;

namespace Breakout3D.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Collider))]
    public class Ball : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip ballHitsWallClip;
        [SerializeField] private AudioClip ballHitsBrickClip;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<Brick>(out _))
            {
                audioSource.PlayOneShot(ballHitsBrickClip);
            }
            else
            {
                audioSource.PlayOneShot(ballHitsWallClip);
            }
        }
    }
}
