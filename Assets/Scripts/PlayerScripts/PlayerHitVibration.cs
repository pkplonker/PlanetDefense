using StuartHeathTools;
using UnityEngine;

namespace PlayerScripts
{
	public class PlayerHitVibration : MonoBehaviour
	{
		private PlayerHealth playerHealth;

		private void Awake()
		{
			playerHealth = GetComponent<PlayerHealth>();
			if (playerHealth == null)
			{
				Debug.LogError("Failed to get playerHealth " + name);
				Destroy(gameObject);
			}
		}

		private void OnEnable()
		{
			playerHealth.onTakeDamage += Hit;
		}

		private void OnDisable()
		{
			playerHealth.onTakeDamage -= Hit;
		}

		private void Hit()
		{
			if (PlayerPrefs.HasKey("Vibration"))
			{
				if (PlayerPrefs.GetInt("Vibration") == 1)
				{

					Vibrate();
				}
			}
			else
			{
				PlayerPrefs.SetInt("Vibration", 1);
				Vibrate();
				PlayerPrefs.Save();
			}
		}

		private static void Vibrate()
		{
#if UNITY_ANDROID
				Handheld.Vibrate();
#endif
		}
	}
}