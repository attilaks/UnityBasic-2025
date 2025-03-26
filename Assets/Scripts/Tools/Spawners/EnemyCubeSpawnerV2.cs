using System;
using System.Collections.Generic;
using Characters;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tools.Spawners
{
	public class EnemyCubeSpawnerV2 : MonoBehaviour
	{
		[SerializeField] private EnemyCube enemyCubePrefab;
		[SerializeField] private byte maxEnemyCount;
		[SerializeField] private float spawnInterval;

		// [SerializeField] private List<Transform> spawnPoints;
		
		private Vector2 _groundSize;
		private EnemyCube _currentEnemyCube;
		private float _lastEnemySpawnTimeDelta;

		private byte _enemiesOnSceneCount;
		private Transform[] _spawnPoints;
		private Dictionary<Transform, bool> _canSpawn = new();

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

			if (_lastEnemySpawnTimeDelta > spawnInterval && _enemiesOnSceneCount < maxEnemyCount)
			{
				SpawnEnemyCube();
			}
		}

		// private void OnDrawGizmos()
		// {
		// 	if (_spawnPoints == null) return;
		// 	
		// 	for (var i = 0; i < _spawnPoints.Length; i++)
		// 	{
		// 		Gizmos.DrawSphere(_spawnPoints[i].position, 0.2f);
		// 	}
		// }

		private void SpawnEnemyCube()
		{
			// Vector3 randomPosition = new Vector3(
			// 	Random.Range(-_groundSize.x / 2, _groundSize.x / 2),
			// 	0.5f,
			// 	Random.Range(-_groundSize.y / 2, _groundSize.y / 2)
			// );
			
			var randomPosition = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
			
			_currentEnemyCube = Instantiate(enemyCubePrefab, randomPosition, Quaternion.identity);
			_currentEnemyCube.OnDeathWithMe += OnEnemyDead;
			++_enemiesOnSceneCount;

			_lastEnemySpawnTimeDelta = 0;
		}

		private void OnEnemyDead(EnemyCube dyingCube)
		{
			dyingCube.OnDeathWithMe -= OnEnemyDead;
			--_enemiesOnSceneCount;
		}
	}
}