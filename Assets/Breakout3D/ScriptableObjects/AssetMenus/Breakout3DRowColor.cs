using System;
using UnityEngine;

namespace Breakout3D.ScriptableObjects.AssetMenus
{
	[Serializable]
	[CreateAssetMenu(menuName = "ScriptableObjects/Breakout3D", fileName = "NewRowColorData")]
	public class Breakout3DRowColor : ScriptableObject
	{
		[SerializeField] private Color color1Row;
		[SerializeField] private Color color2Row; 
		[SerializeField] private Color color3Row; 
		[SerializeField] private Color color4Row; 
		[SerializeField] private Color color5Row; 
		[SerializeField] private Color color6Row; 
		[SerializeField] private Color color7Row;

		private const int MaxColorRow = 7;

		public Color GetColor(int row)
		{
			if (row is <= 0 or > MaxColorRow) return Color.white;
			
			var colorRow = row % (MaxColorRow + 1);

			Color color = colorRow switch
			{
				1 => color1Row,
				2 => color2Row,
				3 => color3Row,
				4 => color4Row,
				5 => color5Row,
				6 => color6Row,
				7 => color7Row,
				_ => throw new ArgumentOutOfRangeException()
			};

			return color;
		}
	}
}