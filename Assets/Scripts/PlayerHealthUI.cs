using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI tmp;
	[SerializeField] private Entity player;
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
