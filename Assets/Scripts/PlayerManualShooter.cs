using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManualShooter : MonoBehaviour
{
	[SerializeField] private Transform targetReticle;
	[SerializeField] private float shootFrequency;
	[SerializeField] private float shootRadius;
	[SerializeField] private Camera cam;
	[SerializeField] private ProjectileData projectileData;
	private PlayerCombatManager playerCombatManager;
	private float shootTimer;
	private bool inGame = false;
	private void OnEnable() => GameManager.onStateChange += StateChange;
	private void OnDisable() => GameManager.onStateChange -= StateChange;
	private void Start() => playerCombatManager = GetComponent<PlayerCombatManager>();


	private void StateChange(GameState state)
	{
		if (state == GameState.InGame)
		{
			SetInGame(true);
		}
		else
		{
			SetInGame(false);
		}
	}

	private void SetInGame(bool inGame)
	{
		targetReticle.gameObject.SetActive(inGame);
		shootTimer = .1f;
		this.inGame = inGame;
	}


	private void Update()
	{
		shootTimer -= Time.deltaTime;
		UpdateReticlePosition();
		if (Input.GetMouseButtonDown(0) && shootTimer <= 0 && inGame && !IsClickingOnUI())

		{
			playerCombatManager.Shoot((targetReticle.position - transform.position).normalized, projectileData);
			shootTimer = shootFrequency;
		}
	}

	private bool IsClickingOnUI()
	{
		PointerEventData eventDataPos = new PointerEventData(EventSystem.current);
		eventDataPos.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataPos, results);
		return results.Capacity > 0;
	}


	private void UpdateReticlePosition()
	{
		var pos = cam.ScreenToWorldPoint(Input.mousePosition);
		pos.z = 0;
		pos = pos.normalized;
		pos *= shootRadius;
		targetReticle.position = pos;
	}
}