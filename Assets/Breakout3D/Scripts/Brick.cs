using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Breakout3D.Scripts
{
	public class Brick : MonoBehaviour
	{
		[SerializeField] private MeshRenderer meshRenderer;
		[SerializeField] private byte hitPoints = 1;
		
		public event Action<Brick> OnHitByBall = delegate { }; 
		
		public byte HitPoints => hitPoints;

		public void SetColor(Color color)
		{
			meshRenderer.material.color = color;
		}

		private void OnCollisionEnter(Collision other)
		{
			if (other.gameObject.TryGetComponent<Ball>(out var ball))
			{
				--hitPoints;
				OnHitByBall.Invoke(this);
			}
		}
	}
}