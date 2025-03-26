using System.Collections.Generic;
using Characters;
using UnityEngine;

namespace Tools.Spawners
{
	public class EnemyCubeSpawner : MonoBehaviour
	{
		[SerializeField] private EnemyCube enemyCubePrefab;
		[SerializeField] private EnemySpawnLocationSetType enemySpawnLocationSetType;
		[SerializeField] private byte maxEnemyCount;
		[SerializeField] private float maxSpawnInterval;
		
		private Vector2 _groundSize;
		private EnemyCube _currentEnemyCube;
		private float _lastEnemySpawnTimeDelta;

		private byte _enemiesOnSceneCount;
		private Transform[] _spawnPoints;
		private Dictionary<Transform, bool> _canSpawn = new();
		private Dictionary<EnemyCube, bool> _canDestroy = new();
		
		private void Awake()
		{
			_spawnPoints = gameObject.GetComponentsInChildren<Transform>();
		}
		
		private void Start()
		{
			if (TryGetComponent<Collider>(out var groundCollider))
			{
				_groundSize = new Vector2(groundCollider.bounds.size.x, groundCollider.bounds.size.z);
				SpawnEnemyCube();
			}
		}
		
		private void Update() 
		{
			_lastEnemySpawnTimeDelta += Time.deltaTime;

			if (_lastEnemySpawnTimeDelta > maxSpawnInterval && _enemiesOnSceneCount < maxEnemyCount)
			{
				SpawnEnemyCube();
			}
		}

		private void SpawnEnemyCube()
		{
			Vector3 randomPosition = new Vector3(
				Random.Range(-_groundSize.x / 2, _groundSize.x / 2),
				0.5f,
				Random.Range(-_groundSize.y / 2, _groundSize.y / 2)
			);

			if (_currentEnemyCube)
				_currentEnemyCube.OnDeath -= SpawnEnemyCube;
			
			_currentEnemyCube = Instantiate(enemyCubePrefab, randomPosition, Quaternion.identity);
			_currentEnemyCube.OnDeath += SpawnEnemyCube;
		}

		private enum EnemySpawnLocationSetType
		{
			FullyRandom = 0,
			InPreparedSpawnPoints = 1
		}
	}
}