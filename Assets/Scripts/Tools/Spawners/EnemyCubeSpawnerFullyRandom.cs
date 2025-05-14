using Characters;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tools.Spawners
{
	[RequireComponent(typeof(Collider))]
	public class EnemyCubeSpawnerFullyRandom : Spawner
	{
		[SerializeField] private byte enemyReservePoolSize = 5;
		
		private Vector2 _groundSize;
		private byte _currentEnemyPoolIndex;
		private EnemyCube[] _enemyReservePool;
		
		private void Awake()
		{
			var groundCollider = GetComponent<Collider>();
			_groundSize = new Vector2(groundCollider.bounds.size.x, groundCollider.bounds.size.z);
			
			_enemyReservePool = new EnemyCube[enemyReservePoolSize];
			for (var i = 0; i < _enemyReservePool.Length; i++)
			{
				_enemyReservePool[i] = Instantiate(enemyCubePrefab);
				_enemyReservePool[i].gameObject.SetActive(false);
				_enemyReservePool[i].OnDeath += SpawnEnemyCube;
			}
		}
		
		private void Start()
		{
			SpawnEnemyCube(0);
		}

		private void OnDestroy()
		{
			for (var i = 0; i < _enemyReservePool.Length; i++)
			{
				_enemyReservePool[i].OnDeath -= SpawnEnemyCube;
			}
		}

		private void SpawnEnemyCube(int enemyRuntimeId)
		{
			var randomPosition= new Vector3(
				Random.Range(-_groundSize.x / 2, _groundSize.x / 2),
				0.5f,
				Random.Range(-_groundSize.y / 2, _groundSize.y / 2)
			);

			if (_currentEnemyPoolIndex >= _enemyReservePool.Length)
			{
				_currentEnemyPoolIndex = 0;
			}
			
			var currentEnemy = _enemyReservePool[_currentEnemyPoolIndex];
			currentEnemy.Restore(randomPosition, null);
			++_currentEnemyPoolIndex;
		}
	}
}