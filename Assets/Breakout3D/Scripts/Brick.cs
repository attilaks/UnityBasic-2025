using System;
using UnityEngine;

namespace Breakout3D.Scripts
{
	public class Brick : MonoBehaviour
	{
		[SerializeField] private MeshRenderer meshRenderer;
		[SerializeField] private byte threshold = 1;
		
		public event Action<Brick> OnHitByBall = delegate { }; 
		
		public byte Threshold => threshold;

		public void SetColor(Color color)
		{
			meshRenderer.material.color = color;
		}

		private void OnCollisionEnter(Collision other)
		{
			if (other.gameObject.TryGetComponent<Ball>(out var ball))
			{
				--threshold;
				OnHitByBall.Invoke(this);
			}
		}
	}
}