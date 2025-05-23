﻿using System;
using System.Collections.Generic;
using Breakout3D.ScriptableObjects.AssetMenus;
using UnityEngine;

namespace Breakout3D.Scripts
{
	public class BricksController : MonoBehaviour
	{
		[SerializeField] private Breakout3DRowColor rowColorData;
		[SerializeField] private GameObject brickPrefab;
		[Range(1, 10)] [SerializeField] private byte rowCount;
		[Range(4, 20)] [SerializeField] private byte columnCount;
		
		public event Action OnBricksCreated = delegate { };
		public event Action<ushort> OnAllBricksDestroyed = delegate { };

		private List<GameObject> _bricks;
		private ushort _bricksDestroyedCount;

		private void Awake()
		{
			_bricks = new List<GameObject>(rowCount * rowCount);
			
			CreateBricks();
		}
		
		private void CreateBricks()
		{
			const float brickActualSizeProportion = 0.9f;
			
			var relativeBrickSizeX = 1f / columnCount;
			var relativeBrickSizeY = Math.Clamp(1f / rowCount, 0f, 0.5f);
			
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
					var brickComponent = brick.GetComponent<Brick>();
					brickComponent.SetColor(color);
					brickComponent.OnHitByBall += OnBrickHitByWall;

					var scale = new Vector3(relativeBrickSizeX * brickActualSizeProportion, relativeBrickSizeY * brickActualSizeProportion, 1f);
					brick.transform.localScale = Vector3.Scale(brick.transform.localScale, scale);
					
					_bricks.Add(brick);
				}
			}
			
			OnBricksCreated.Invoke();
		}

		private void OnBrickHitByWall(Brick brickComponent)
		{
			if (brickComponent.HitPoints > 0) return;
			
			brickComponent.OnHitByBall -= OnBrickHitByWall;
			_bricks.Remove(brickComponent.gameObject);
			Destroy(brickComponent.gameObject, 0.1f);
			++_bricksDestroyedCount;

			if (_bricks.Count == 0)
			{
				OnAllBricksDestroyed.Invoke(_bricksDestroyedCount);
				CreateBricks();
				_bricksDestroyedCount = 0;
			}
		}
	}
}