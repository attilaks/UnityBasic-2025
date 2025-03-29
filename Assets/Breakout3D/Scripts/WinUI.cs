using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Breakout3D.Scripts
{
	public class WinUI : MonoBehaviour
	{
		[Header("Event invokers")]
		[SerializeField] private BricksController bricksController;
		
		[Header("CanvasGroups")]
		[SerializeField] private CanvasGroup winUiCanvasGroup;
		[SerializeField] private CanvasGroup titleCanvasGroup;
		[SerializeField] private CanvasGroup cupsCanvasGroup;
		[SerializeField] private CanvasGroup coinsCanvasGroup;
		[SerializeField] private CanvasGroup collectButtonCanvasGroup;
		[SerializeField] private CanvasGroup advButtonCanvasGroup;
		
		[Header("Images")]
		[SerializeField] private Image backgroundImage;

		[Header("Texts")]
		[SerializeField] private TextMeshProUGUI coinsText;
		
		[Header("Buttons")]
		[SerializeField] private Button collectButton;
		[SerializeField] private Button advertisementButton;
		
		[Header("Settings")]
		[SerializeField] private float animationDuration = 1f;
		
		public static bool IsGameInputBlocked { get; private set; } = false;
		
		private void Awake()
		{
			gameObject.SetActive(false);
			
			bricksController.OnAllBricksDestroyed += OnAllBricksDestroyed;
			
			collectButton.onClick.AddListener(OnCollectButtonClicked);
			advertisementButton.onClick.AddListener(OnAdvertisementButtonClicked);
		}

		private void OnDestroy()
		{
			bricksController.OnAllBricksDestroyed -= OnAllBricksDestroyed;
			
			collectButton.onClick.RemoveListener(OnCollectButtonClicked);
			advertisementButton.onClick.RemoveListener(OnAdvertisementButtonClicked);
		}
		
		private void OnAllBricksDestroyed(ushort bricksDestroyedCount)
		{
			coinsText.text = bricksDestroyedCount.ToString();
			PlayOnEnableSequence();
		}

		private void OnCollectButtonClicked()
		{
			PlayOnDisableSequence();
		}
		
		private void OnAdvertisementButtonClicked()
		{
			Debug.LogWarning("Здесь будет реклама.");
		}
		
		[ContextMenu("Play On Enable Sequence")]
		private void PlayOnEnableSequence()
		{
			IsGameInputBlocked = true;
			
			DOTween.Sequence()
				.OnStart(() =>
				{
					winUiCanvasGroup.gameObject.SetActive(true);
					winUiCanvasGroup.alpha = 1f;
				})
				.Append(backgroundImage.DOFade(0.6f, animationDuration).From(0f))
				.Append(titleCanvasGroup.DOFade(1f, animationDuration).From(0f))
				.Append(cupsCanvasGroup.transform.DOScale(1f, animationDuration).From(20))
				.Append(coinsCanvasGroup.transform.DOScale(1f, animationDuration).From(20))
				.Append(collectButtonCanvasGroup.transform.DOScale(1f, animationDuration).From(0f))
					.Join(collectButtonCanvasGroup.DOFade(1f, animationDuration).From(0f))
				.Append(advButtonCanvasGroup.transform.DOScale(1f, animationDuration).From(0f))
					.Join(advButtonCanvasGroup.DOFade(1f, animationDuration).From(0f))
				
				.Play();
		}
		
		private void PlayOnDisableSequence()
		{
			DOTween.Sequence()
				.Append(winUiCanvasGroup.DOFade(0f, animationDuration))
				.OnComplete(() => gameObject.SetActive(false))
				.Play();
			
			IsGameInputBlocked = false;
		}
	}
}