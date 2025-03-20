using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Breakout3D.Scripts
{
	public class WinUI : MonoBehaviour
	{
		[SerializeField] private BricksController bricksController;
		
		[SerializeField] private Image backgroundImage;
		[SerializeField] private TextMeshProUGUI coinsText;
		[SerializeField] private Button collectButton;
		[SerializeField] private Button advertisementButton;
		
		private void Awake()
		{
			bricksController.OnAllBricksDestroyed += OnAllBricksDestroyed;
			collectButton.onClick.AddListener(OnCollectButtonClicked);
			advertisementButton.onClick.AddListener(OnAdvertisementButtonClicked);
			gameObject.SetActive(true); // todo заменить на false
			
			backgroundImage.DOFade(0, 3f);
		}

		private void OnDestroy()
		{
			bricksController.OnAllBricksDestroyed -= OnAllBricksDestroyed;
			collectButton.onClick.RemoveListener(OnCollectButtonClicked);
			advertisementButton.onClick.RemoveListener(OnAdvertisementButtonClicked);
		}
		
		private void OnAllBricksDestroyed(ushort bricksDestroyedCount)
		{
			gameObject.SetActive(true);
			coinsText.text = bricksDestroyedCount.ToString();
			//TODO
		}
		
		private void OnCollectButtonClicked()
		{
			gameObject.SetActive(false);
			//todo
		}
		
		private void OnAdvertisementButtonClicked()
		{
			Debug.LogWarning("Здесь будет реклама.");
		}
	}
}