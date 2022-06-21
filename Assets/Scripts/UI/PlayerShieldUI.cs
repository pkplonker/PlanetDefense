using System;
using StuartHeathTools;

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
			if (player.IsShieldUnlocked())
			{
				CheckNeedToFlash(currentHealth, maxHealth);
				tmp.text = Utility.FormatMoneyToKMB((ulong) currentHealth) + "/" +
				           Utility.FormatMoneyToKMB((ulong) maxHealth);
				icon.enabled = true;
				icon.color = defaultImageColor;
				if (currentHealth != 0 || cor == null) return;
				StopCoroutine(cor);
				icon.color = lowHealthColor;
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