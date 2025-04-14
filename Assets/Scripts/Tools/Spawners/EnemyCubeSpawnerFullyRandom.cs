using Characters;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tools.Spawners
{
	[RequireComponent(typeof(Collider))]
	public class EnemyCubeSpawnerFullyRandom : Spawner
	{
		private Vector2 _groundSize;
		private EnemyCube _currentEnemyCube;
		
		private void Awake()
		{
			var groundCollider = GetComponent<Collider>();
			_groundSize = new Vector2(groundCollider.bounds.size.x, groundCollider.bounds.size.z);
		}
		
		private void Start()
		{
			SpawnEnemyCube(0);
		}

		private void SpawnEnemyCube(int enemyRuntimeId)
		{
			var randomPosition= new Vector3(
				Random.Range(-_groundSize.x / 2, _groundSize.x / 2),
				0.5f,
				Random.Range(-_groundSize.y / 2, _groundSize.y / 2)
			);
			
			if (_currentEnemyCube)
				_currentEnemyCube.OnDeath -= SpawnEnemyCube;
			
			_currentEnemyCube = Instantiate(enemyCubePrefab, randomPosition, Quaternion.identity);
			_currentEnemyCube.OnDeath += SpawnEnemyCube;
		}
	}
}