using Characters;
using UnityEngine;

namespace Tools.Spawners
{
	public class SpawnPoint : MonoBehaviour
	{
		private EnemyCube _enemy;
		
		private void Awake()
		{
			var children = gameObject.GetComponentsInChildren<Transform>();
			for (var i = 0; i < children.Length; i++)
			{
				if (children[i] == transform) continue;
				Destroy(children[i].gameObject);
			}
		}

		private void OnDestroy()
		{
			if (_enemy) _enemy.OnDeath -= OnEnemyDead;
		}

		private void OnTransformChildrenChanged()
		{
			for (var i = transform.childCount - 1; i >= 0; i--)
			{
				var child = transform.GetChild(i);
				var enemy = child.GetComponent<EnemyCube>();
				
				if (!enemy || _enemy != enemy)
				{
					Destroy(child.gameObject);
				}
				else 
				{
					_enemy = enemy;
				}
			}
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red; 
			Gizmos.DrawSphere(transform.position, 0.5f);
		}

		public EnemyCube SpawnEnemy(EnemyCube enemyCubePrefab)
		{
			if (!_enemy)
			{
				_enemy = Instantiate(enemyCubePrefab, transform.position, Quaternion.identity);
				_enemy.transform.SetParent(transform);
				_enemy.OnDeath += OnEnemyDead;
			}
			else if (!_enemy.gameObject.activeInHierarchy)
			{
				_enemy.Restore(transform.position, transform);
			}

			return _enemy;
		}

		private void OnEnemyDead(int enemyRuntimeId)
		{
			_enemy.transform.SetParent(null);
		}
	}
}