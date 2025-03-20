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
		
		[Header("Images")]
		[SerializeField] private Image backgroundImage;
		[SerializeField] private Image titleImage;
		[SerializeField] private Image collectButtonFrontImage;
		[SerializeField] private Image collectButtonBottomImage;
		[SerializeField] private Image collectButtonTopImage;
		[SerializeField] private Image collectButtonShadowImage;
		[SerializeField] private Image collectButtonGradientImage;
		[SerializeField] private Image advButtonFrontImage;
		[SerializeField] private Image advButtonBottomImage;
		[SerializeField] private Image advButtonTopImage;
		[SerializeField] private Image advButtonShadowImage;
		[SerializeField] private Image advButtonGradientImage;
		[SerializeField] private Image cupsContainerImage;
		[SerializeField] private Image coinsContainerImage;
		
		[Header("Texts")]
		[SerializeField] private TextMeshProUGUI coinsText;
		[SerializeField] private TextMeshProUGUI titleText;
		[SerializeField] private TextMeshProUGUI collectButtonText;
		[SerializeField] private TextMeshProUGUI advButtonText1;
		[SerializeField] private TextMeshProUGUI advButtonText2;
		[SerializeField] private TextMeshProUGUI cupsContainerText;
		[SerializeField] private TextMeshProUGUI coinsContainerText;
		
		[Header("Transforms")]
		[SerializeField] private Transform cupsContainer;
		[SerializeField] private Transform coinsContainer;
		
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

		private void SetEnableSequenceStartPosition()
		{
			gameObject.SetActive(true);
			
			backgroundImage.color = SetColorTransparency(backgroundImage.color);
			
			titleImage.gameObject.SetActive(false);
			titleImage.color = SetColorTransparency(titleImage.color);
			titleText.color = SetColorTransparency(titleText.color);
			
			cupsContainer.gameObject.SetActive(false);
			cupsContainer.localScale *= 10;
			cupsContainerImage.color = SetColorTransparency(cupsContainerImage.color, 1);
			cupsContainerText.color = SetColorTransparency(cupsContainerText.color, 1);
			
			coinsContainer.gameObject.SetActive(false);
			coinsContainer.localScale *= 10;
			coinsContainerImage.color = SetColorTransparency(coinsContainerImage.color, 1);
			coinsContainerText.color = SetColorTransparency(coinsContainerText.color, 1);
			
			collectButton.gameObject.SetActive(false);
			collectButton.transform.localScale = Vector3.zero;
			collectButtonText.color = SetColorTransparency(collectButtonText.color);
			collectButtonFrontImage.color = SetColorTransparency(collectButtonFrontImage.color);
			collectButtonBottomImage.color = SetColorTransparency(collectButtonBottomImage.color);
			collectButtonTopImage.color = SetColorTransparency(collectButtonTopImage.color);
			collectButtonShadowImage.color = SetColorTransparency(collectButtonShadowImage.color);
			collectButtonGradientImage.color = SetColorTransparency(collectButtonGradientImage.color);
			
			advertisementButton.gameObject.SetActive(false);
			advertisementButton.transform.localScale = Vector3.zero;
			advButtonText1.color = SetColorTransparency(advButtonText1.color);
			advButtonText2.color = SetColorTransparency(advButtonText2.color);
			advButtonFrontImage.color = SetColorTransparency(advButtonFrontImage.color);
			advButtonBottomImage.color = SetColorTransparency(advButtonBottomImage.color);
			advButtonTopImage.color = SetColorTransparency(advButtonTopImage.color);
			advButtonShadowImage.color = SetColorTransparency(advButtonShadowImage.color);
			advButtonGradientImage.color = SetColorTransparency(advButtonGradientImage.color);
		}

		private Color SetColorTransparency(Color color, float alpha = 0)
		{
			color.a = alpha;
			return color;
		}

		private void OnCollectButtonClicked()
		{
			PlayOnDisableSequence();
		}
		
		private void OnAdvertisementButtonClicked()
		{
			Debug.LogWarning("Здесь будет реклама.");
		}
		
		private void PlayOnEnableSequence()
		{
			IsGameInputBlocked = true;
			
			DOTween.Sequence()
				.OnStart(SetEnableSequenceStartPosition)
				
				.Append(backgroundImage.DOFade(0.6f, animationDuration))
				
				.Append(titleImage.DOFade(1f, animationDuration)
					.OnStart(() => titleImage.gameObject.SetActive(true)))
				.Join(titleText.DOFade(1f, animationDuration))
				
				.Append(cupsContainer.DOScale(1f, animationDuration)
					.OnStart(() => cupsContainer.gameObject.SetActive(true)))
				.Append(coinsContainer.DOScale(1f, animationDuration)
					.OnStart(() => coinsContainer.gameObject.SetActive(true)))
				
				.Append(collectButton.transform.DOScale(1f, animationDuration)
					.OnStart(() => collectButton.gameObject.SetActive(true)))
				.Join(collectButtonText.DOFade(1f, animationDuration))
				.Join(collectButtonFrontImage.DOFade(1f, animationDuration))
				.Join(collectButtonBottomImage.DOFade(1f, animationDuration))
				.Join(collectButtonTopImage.DOFade(1f, animationDuration))
				.Join(collectButtonShadowImage.DOFade(0.8f, animationDuration))
				.Join(collectButtonGradientImage.DOFade(0.6f, animationDuration))
				
				.Join(advertisementButton.transform.DOScale(1f, animationDuration)
					.OnStart(() => advertisementButton.gameObject.SetActive(true)))
				.Join(advButtonText1.DOFade(1f, animationDuration))
				.Join(advButtonText2.DOFade(0.8f, animationDuration))
				.Join(advButtonFrontImage.DOFade(1f, animationDuration))
				.Join(advButtonBottomImage.DOFade(1f, animationDuration))
				.Join(advButtonTopImage.DOFade(1f, animationDuration))
				.Join(advButtonShadowImage.DOFade(0.8f, animationDuration))
				.Join(advButtonGradientImage.DOFade(0.6f, animationDuration))
				
				.Play();
		}
		
		private void PlayOnDisableSequence()
		{
			DOTween.Sequence()
				.Append(backgroundImage.DOFade(0f, animationDuration))
				.Join(titleImage.DOFade(0f, animationDuration))
				.Join(titleText.DOFade(0f, animationDuration))
				.Join(cupsContainerImage.DOFade(0f, animationDuration))
				.Join(cupsContainerText.DOFade(0f, animationDuration))
				.Join(coinsContainerImage.DOFade(0f, animationDuration))
				.Join(coinsContainerText.DOFade(0f, animationDuration))
				.Join(collectButtonText.DOFade(0f, animationDuration))
				.Join(collectButtonFrontImage.DOFade(0f, animationDuration))
				.Join(collectButtonBottomImage.DOFade(0f, animationDuration))
				.Join(collectButtonTopImage.DOFade(0f, animationDuration))
				.Join(collectButtonShadowImage.DOFade(0f, animationDuration))
				.Join(collectButtonGradientImage.DOFade(0f, animationDuration))
				.Join(advButtonText1.DOFade(0f, animationDuration))
				.Join(advButtonText2.DOFade(0f, animationDuration))
				.Join(advButtonFrontImage.DOFade(0f, animationDuration))
				.Join(advButtonBottomImage.DOFade(0f, animationDuration))
				.Join(advButtonTopImage.DOFade(0f, animationDuration))
				.Join(advButtonShadowImage.DOFade(0f, animationDuration))
				.Join(advButtonGradientImage.DOFade(0f, animationDuration))
				.OnComplete(() => gameObject.SetActive(false))
				.Play();
			
			IsGameInputBlocked = false;
		}
	}
}