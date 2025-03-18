using System;
using UnityEngine;

namespace Breakout3D
{
	[RequireComponent(typeof(BoxCollider))]
	public class BottomWall : MonoBehaviour
	{
		public event Action OnBallHitBottomWall = delegate { };
		
		private void OnCollisionEnter(Collision other)
		{
			if (other.gameObject.TryGetComponent<Ball>(out var ball))
			{
				OnBallHitBottomWall?.Invoke();
			}
		}
	}
}
