using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Breakout3D
{
	public class Brick : MonoBehaviour
	{
		[SerializeField] private MeshRenderer meshRenderer;
		
		public event Action<Brick> OnHitByBall = delegate { }; 

		public void SetColor(Color color)
		{
			meshRenderer.material.color = color;
		}

		private void OnCollisionEnter(Collision other)
		{
			if (other.gameObject.TryGetComponent<Ball>(out var ball))
			{
				OnHitByBall.Invoke(this);
			}
		}
	}
}