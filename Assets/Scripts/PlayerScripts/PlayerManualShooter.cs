using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Upgrades;

namespace PlayerScripts
{
	public class PlayerManualShooter : MonoBehaviour
	{
		[SerializeField] private Transform targetReticule;
		[SerializeField] private Stat manualShootSpeed;
		[SerializeField] private float shootRadius;
		[SerializeField] private Camera cam;
		[SerializeField] private ProjectileData projectileData;
		private PlayerCombatManager playerCombatManager;
		private float shootTimer;
		private bool inGame;
		private void OnEnable() => GameManager.onStateChange += StateChange;
		private void OnDisable() => GameManager.onStateChange -= StateChange;
		private void Start() => playerCombatManager = GetComponent<PlayerCombatManager>();
		private void StateChange(GameState state) => SetInGame(state == GameState.InGame);


		private void SetInGame(bool inGame)
		{
			targetReticule.gameObject.SetActive(inGame);
			shootTimer = .1f;
			this.inGame = inGame;
		}


		private void Update()
		{
			if (!inGame)
			{
				targetReticule.gameObject.SetActive(false);
				return;
			}

			targetReticule.gameObject.SetActive(true);

			shootTimer -= Time.deltaTime;
			UpdateReticulePosition();
			if (!Input.GetMouseButtonDown(0) || !(shootTimer <= 0) || !inGame || IsClickingOnUI()) return;
			playerCombatManager.Shoot((targetReticule.position - transform.position).normalized, projectileData);
			shootTimer = manualShootSpeed.GetCurrentValue();
		}

		private bool IsClickingOnUI()
		{
			var eventDataPos = new PointerEventData(EventSystem.current)
			{
				position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
			};
			var results = new List<RaycastResult>();
			EventSystem.current.RaycastAll(eventDataPos, results);
			return results.Capacity > 0;
		}


		private void UpdateReticulePosition()
		{
			var pos = cam.ScreenToWorldPoint(Input.mousePosition);
			pos.z = 0;
			pos = pos.normalized;
			pos *= shootRadius;
			targetReticule.position = pos;
		}
	}
}