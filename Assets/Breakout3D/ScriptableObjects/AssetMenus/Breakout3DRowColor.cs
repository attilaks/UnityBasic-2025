using System;
using UnityEngine;

namespace Breakout3D.ScriptableObjects.AssetMenus
{
	[Serializable]
	[CreateAssetMenu(menuName = "ScriptableObjects/Breakout3D", fileName = "NewRowColorData")]
	public class Breakout3DRowColor : ScriptableObject
	{
		[SerializeField] private Color[] colors;

		public Color GetColor(int row)
		{
			if (row <= 0) return Color.white;
			
			var colorIndex = (row - 1) % colors.Length;
			return colors[colorIndex];
		}
	}
}