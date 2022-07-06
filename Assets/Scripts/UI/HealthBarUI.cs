using System.Collections;
using PlayerScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public abstract class HealthBarUI : MonoBehaviour
	{
		[Range(0, 1)] [SerializeField] protected float lowHealthThreshold = 0.2f;
		[SerializeField] protected PlayerController player;
		[SerializeField] protected Color lowHealthColor;
		protected Color defaultColor;
		protected TextMeshProUGUI tmp;
		protected Coroutine cor;
		[SerializeField] protected Image icon;
		[SerializeField] protected Color defaultImageColor;
		[Range(0, 1)] protected float flashRate;

		protected virtual void Awake()
		{
			tmp = GetComponent<TextMeshProUGUI>();
			defaultColor = tmp.color;
		}

		protected virtual void OnEnable()=>GameManager.onStateChange += GameManagerOnonStateChange;
		
		

		protected virtual void OnDisable()=>GameManager.onStateChange -= GameManagerOnonStateChange;


		private void GameManagerOnonStateChange(GameState state)
		{
			icon.color = defaultImageColor;
			tmp.color = defaultImageColor;
			if (cor == null) return;
			StopCoroutine(cor);
			cor = null;
		}

		private IEnumerator FlashCor(Graphic image, Color defaultColor, Color flashColor,
			float flashDuration = 0.1f)
		{
			float timer = 0;
			while (true)
			{
				timer += Time.deltaTime;
				if (timer > flashDuration * SpeedButton.GetSpeedMultiplier())
				{
					timer = 0;
					image.color = image.color == defaultColor ? flashColor : defaultColor;
				}
				yield return null;
			}
		}

		protected abstract void UpdateUI(float currentHealth, float maxHealth);

		protected virtual void CheckNeedToFlash(float currentHealth, float maxHealth)
		{
			if (currentHealth / maxHealth < lowHealthThreshold)
			{
				cor ??= StartCoroutine(FlashCor(icon, defaultImageColor, lowHealthColor));
				tmp.color = lowHealthColor;
			}
			else
			{
				if (cor != null) StopCoroutine(cor);
				tmp.color = defaultColor;
			}
		}
	}
}