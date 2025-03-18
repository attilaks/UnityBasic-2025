using System.Collections.Generic;
using ScriptableObjects.AssetMenus;
using UnityEngine;

namespace Breakout3D
{
	public class BricksController : MonoBehaviour
	{
		[SerializeField] private Breakout3DRowColor rowColorData;
		[SerializeField] private GameObject brickPrefab;
		[Range(1, 10)] [SerializeField] private byte rowCount;
		[Range(4, 20)] [SerializeField] private byte columnCount;

		private List<GameObject> _bricks;

		private void Awake()
		{
			_bricks = new List<GameObject>(rowCount * rowCount);
			
			CreateBricks();
		}
		
		private void CreateBricks()
		{
			const float interval = 1f / 10;
			var relativeBrickSizeX = 1f / columnCount;
			var relativeBrickSizeY = 1f / rowCount;
			
			var startPosition = new Vector3(relativeBrickSizeX / 2 - 0.5f, relativeBrickSizeY / 2 + 0.5f, 0f);
			
			for (var row = 1; row <= rowCount; row++)
			{
				for (var col = 0; col < columnCount; col++)
				{
					var deltaPosition = new Vector3(col * relativeBrickSizeX, -row * relativeBrickSizeY, 0f);
					var brickPosition = startPosition + deltaPosition;
					
					var brick = Instantiate(brickPrefab, transform);
					brick.transform.localPosition = brickPosition;
					
					var color = rowColorData.GetColor(row);
					brick.GetComponent<Brick>().SetColor(color);

					var scale = new Vector3(relativeBrickSizeX * (1f - interval), relativeBrickSizeY * (1f - interval), 1f);
					brick.transform.localScale = Vector3.Scale(brick.transform.localScale, scale);
				}
			}
		}
	}
}