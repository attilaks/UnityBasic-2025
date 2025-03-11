using Tools;
using Tools.Managers;
using UnityEngine;

namespace Characters
{
	public class Player : MonoBehaviour
	{
		private readonly HealthManagerV0 _healthManagerV0 = new (100f);

		public void PickUpFirstAidKit()
		{
			_healthManagerV0.GetFirstAidKit();
			Debug.LogError($"Я взял аптечку! Теперь у меня их {_healthManagerV0.FirstAidKitCollected} :)");
		}
	}
}