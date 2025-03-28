using System.Collections;
using Characters;
using Tools.CustomCollections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tools.Spawners
{
	public class EnemyCubeSpawnerAtPreparedLocations : Spawner
	{
		[Header("Settings")]
		[SerializeField] private uint maxEnemyCount = 10;
		[SerializeField] private float minSpawnInterval;
		[SerializeField] private float maxSpawnInterval = 2f;
		
		private SpawnPoint[] _spawnPoints;
		private readonly ObservableDictionary<int, EnemyCube> _idEnemyDict = new();

		private void Awake()
		{
			_spawnPoints = gameObject.GetComponentsInChildren<SpawnPoint>();
			
			maxEnemyCount = maxEnemyCount < _spawnPoints.Length ? maxEnemyCount : (uint)_spawnPoints.Length;
			if (minSpawnInterval > maxSpawnInterval)
				maxSpawnInterval = minSpawnInterval;
		}

		private void Start()
		{
			_idEnemyDict.CountChanged += OnEnemyCountChanged;
			StartCoroutine(SpawnEnemyCube());
		}

		private IEnumerator SpawnEnemyCube()
		{
			while (_idEnemyDict.Count < maxEnemyCount)
			{
				if (_spawnPoints.Length == 0) break;
				
				var newRandomSpawnPointIndex = Random.Range(0, _spawnPoints.Length);
				while (_spawnPoints[newRandomSpawnPointIndex].transform.childCount > 0)
				{
					newRandomSpawnPointIndex = Random.Range(0, _spawnPoints.Length);
				}
				var randomEmptySpawnPoint = _spawnPoints[newRandomSpawnPointIndex].transform;
				
				var enemyCube = Instantiate(enemyCubePrefab, randomEmptySpawnPoint.position, Quaternion.identity);
				enemyCube.transform.SetParent(randomEmptySpawnPoint);
				enemyCube.OnDeath += OnEnemyDead;
				_idEnemyDict[enemyCube.RuntimeId] = enemyCube;
				
				var spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
				yield return new WaitForSeconds(spawnInterval);
			}
		}
		
		private void OnEnemyCountChanged(int newEnemyCount)
		{
			// throw new NotImplementedException();
		}

		private void OnEnemyDead(int enemyRuntimeId)
		{
			if (!_idEnemyDict.TryGetValue(enemyRuntimeId, out var enemy)) return;
			
			enemy.OnDeath -= OnEnemyDead;
			_idEnemyDict.Remove(enemyRuntimeId);
		}
	}
}