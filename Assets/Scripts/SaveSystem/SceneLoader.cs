using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SaveSystem
{
	public class SceneLoader : MonoBehaviour
	{
		private void Awake()
		{
			StartCoroutine(LoadScene());
		}

		private IEnumerator LoadScene()
		{
			yield return SceneManager.LoadSceneAsync("Shooting");
		}
	}
}