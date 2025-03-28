using Characters;
using UnityEngine;

namespace Tools.Spawners
{
	public abstract class Spawner : MonoBehaviour
	{
		[Header("Prefab references")]
		[SerializeField] protected EnemyCube enemyCubePrefab;
		
		// При активации отключаем все другие компоненты этого же типа
		private void OnEnable()
		{
			DisableOtherInstances();
		}

		// В редакторе также можно добавить проверку через OnValidate
		private void OnValidate()
		{
			if (enabled)
			{
				DisableOtherInstances();
			}
		}
		
		private void DisableOtherInstances()
		{
			var allInstances = GetComponents<Spawner>();
			for (var i = 0; i < allInstances.Length; i++)
			{
				var instance = allInstances[i];
				if (instance == this || !instance.enabled) continue;
				
				instance.enabled = false;
#if UNITY_EDITOR
				Debug.Log($"Disabled {instance.GetType().Name} because {GetType().Name} is enabled", this);
#endif
			}
		}
	}
}