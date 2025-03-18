using System;
using UnityEngine;

namespace Breakout3D
{
	[RequireComponent(typeof(AudioSource))]
	[RequireComponent(typeof(BoxCollider))]
	public class BottomWall : MonoBehaviour
	{
		[SerializeField] private AudioSource audioSource;
		
		public event Action OnBallHitBottomWall = delegate { };
		
		private void OnCollisionEnter(Collision other)
		{
			if (other.gameObject.TryGetComponent<Ball>(out var ball))
			{
				audioSource.Play();
				OnBallHitBottomWall?.Invoke();
			}
		}
	}
}
