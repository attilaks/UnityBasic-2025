using Characters;
using UnityEngine;

namespace Tools.Spawners
{
	public class EnemyCubeSpawner : MonoBehaviour
	{
		[SerializeField] private EnemyCube enemyCubePrefab;
		
		private Vector2 _groundSize;
		private EnemyCube _currentEnemyCube;
		
		private void Start()
		{
			if (TryGetComponent<Collider>(out var groundCollider))
			{
				_groundSize = new Vector2(groundCollider.bounds.size.x, groundCollider.bounds.size.z);
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
			
			_currentEnemyCube = Instantiate(enemyCubePrefab, randomPosition, Quaternion.identity);
			_currentEnemyCube.OnDeath += SpawnEnemyCube;
		}
	}
}