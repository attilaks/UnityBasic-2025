using UnityEngine;

namespace Tools.Spawners
{
	public class FirstAidKitSpawner : MonoBehaviour
	{
		[SerializeField] private GameObject firstAidKitPrefab;
		[SerializeField] private int numberOfKits = 5;
	
		private Vector2 _groundSize;

		private void Start()
		{
			if (TryGetComponent<Collider>(out var groundCollider))
			{
				_groundSize = new Vector2(groundCollider.bounds.size.x, groundCollider.bounds.size.z);
				SpawnFirstAidKits();
			}
		}

		void SpawnFirstAidKits()
		{
			for (int i = 0; i < numberOfKits; i++)
			{
				Vector3 randomPosition = new Vector3(
					Random.Range(-_groundSize.x / 2, _groundSize.x / 2),
					0.5f,
					Random.Range(-_groundSize.y / 2, _groundSize.y / 2)
				);
			
				Instantiate(firstAidKitPrefab, randomPosition, Quaternion.identity);
			}
		}
	}
}
