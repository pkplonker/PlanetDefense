using System;

namespace UI
{
	public class PlayerShieldUI : HealthBarUI
	{
		protected override void OnEnable()
		{
			base.OnEnable();
			player.onShieldChanged += UpdateUI;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			player.onShieldChanged -= UpdateUI;
		}

		protected override void UpdateUI(float currentHealth, float maxHealth)
		{
			CheckNeedToFlash(currentHealth, maxHealth);

			if (player.IsShieldUnlocked())
			{
				tmp.text = (ulong) currentHealth + "/" + (ulong) maxHealth;
				icon.enabled = true;
				icon.color = defaultImageColor;
			}
			else
			{
				tmp.text = "";
				icon.color = defaultImageColor;

				icon.enabled = false;
			}
		}
	}
}