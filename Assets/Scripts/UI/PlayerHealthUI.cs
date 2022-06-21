using System;
using StuartHeathTools;

namespace UI
{
	public class PlayerHealthUI : HealthBarUI
	{
		protected override void OnEnable()
		{
			base.OnEnable();
			player.onHealthChanged += UpdateUI;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			player.onHealthChanged -= UpdateUI;
		}

		protected override void UpdateUI(float currentHealth, float maxHealth)
		{
			CheckNeedToFlash(currentHealth, maxHealth);
			tmp.text = Utility.FormatMoneyToKMB((ulong) currentHealth) + "/" +
			           Utility.FormatMoneyToKMB((ulong) maxHealth);
			icon.enabled = true;
		}
	}
}