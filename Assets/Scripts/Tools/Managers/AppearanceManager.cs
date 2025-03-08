using System;
using Enums;
using UnityEngine;

namespace Tools.Managers
{
	public class AppearanceManager : MonoBehaviour
	{
		[SerializeField] private MeshRenderer meshRenderer;

		public void SetColor(DiplomacyState diplomacyState)
		{
			var color = diplomacyState switch
			{
				DiplomacyState.Ally => Color.blue,
				DiplomacyState.Enemy => Color.red,
				_ => throw new ArgumentOutOfRangeException()
			};
		
			meshRenderer.sharedMaterial.color = color;
		}
	}
}