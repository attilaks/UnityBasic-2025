using Tools;
using UnityEngine;

namespace Heroes
{
	public class Player : MonoBehaviour
	{
		private readonly HealthManager _healthManager = new (100f);

		public void PickUpFirstAidKit()
		{
			_healthManager.GetFirstAidKit();
			Debug.LogError($"Я взял аптечку! Теперь у меня их {_healthManager.FirstAidKitCollected} :)");
		}
	}
}