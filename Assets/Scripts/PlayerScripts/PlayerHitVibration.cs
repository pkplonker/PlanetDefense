using System;
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
//#if UNITY_ANDROID || UNITY_IOS
			if (!Application.isMobilePlatform) return;
			if (PlayerPrefs.HasKey("Vibration"))
			{
				if (PlayerPrefs.GetInt("Vibration") == 1) Vibrate();
			}
			else
			{
				PlayerPrefs.SetInt("Vibration", 1);
				Vibrate();
				PlayerPrefs.Save();
			}

			
//#endif
		}

		private static void Vibrate()
		{
			Debug.Log("Vibrate");
			Handheld.Vibrate();
		}
	}
}