using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class SliderToggle : MonoBehaviour
	{
		[SerializeField] private Image background;
		[SerializeField] private Image slider;
		[SerializeField] private Sprite backgroundOn;
		[SerializeField] private Sprite backgroundOff;
		[SerializeField] private Sprite sliderOn;
		[SerializeField] private Sprite sliderOff;
		[SerializeField] private string playerPrefsValue = "Vibration";


		private void Awake()
		{
			if (PlayerPrefs.HasKey(playerPrefsValue))
			{
				if (PlayerPrefs.GetInt(playerPrefsValue) == 1) SetSliderOn();
				else SetSliderOff();
			}
			else SetSliderOn();
		}

		public void Slide()
		{
			if (PlayerPrefs.GetInt(playerPrefsValue) == 1) SetSliderOff();
			else SetSliderOn();
		}

		private void SetSliderOff()
		{
			background.sprite = backgroundOff;
			slider.sprite = sliderOff;
			PlayerPrefs.SetInt(playerPrefsValue, 0);
		//	Logger.LogWithColor("Setting " + playerPrefsValue + " off", Color.cyan);
		}

		private void SetSliderOn()
		{
			background.sprite = backgroundOn;
			slider.sprite = sliderOn;
			PlayerPrefs.SetInt(playerPrefsValue, 1);
//			Logger.LogWithColor("Setting " + playerPrefsValue + " on", Color.green);
		}
	}
}