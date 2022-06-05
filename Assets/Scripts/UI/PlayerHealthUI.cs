using TMPro;
using UnityEngine;

namespace UI
{
	public class PlayerHealthUI : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI tmp;
		[SerializeField] private Player player;
		private void OnEnable()
		{
			player.onHealthChanged += UpdateUI;
		}

		private void OnDisable()
		{
			player.onHealthChanged -= UpdateUI;

		}

		private void UpdateUI(float currentHealth)
		{
			tmp.text = currentHealth + "/" + player.GetMaxHealth();
		}
	}
}
