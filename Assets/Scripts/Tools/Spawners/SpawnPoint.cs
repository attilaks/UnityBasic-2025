using Characters;
using UnityEngine;

namespace Tools.Spawners
{
	public class SpawnPoint : MonoBehaviour
	{
		private void Awake()
		{
			var children = gameObject.GetComponentsInChildren<Transform>();
			for (var i = 0; i < children.Length; i++)
			{
				Destroy(children[i].gameObject);
			}
		}

		private void OnTransformChildrenChanged()
		{
			for (var i = transform.childCount - 1; i >= 0; i--)
			{
				var child = transform.GetChild(i);
				
				if (child.GetComponent<EnemyCube>() == null)
				{
					Destroy(child.gameObject);
				}
			}
		}

		private void OnDestroy()
		{
			Debug.LogError("Spawn point is destroyed");
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red; 
			Gizmos.DrawSphere(transform.position, 0.5f);
		}
	}
}