using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerInfo
{
	public string playerName;
	public Entity[] entities;
}

public class BattleStateMachine : MonoStateMachine
{
	[Header("References")]
	public GameObject playerPrefab;
	public GameObject enemyPrefab;
	public BattleUIMode battleUIMode;
	public BattleInitUIMode battleInitUIMode;
	[SerializeField] GameObject[] archetypeButtons;

	[Header("Params")]
	[HideInInspector] public InstaCat[] playerCats;
	[Tooltip("Order must be DPS > TANK > SUPPORT")]
	public InstaCat[] AICats;
	public float coinFlipAnimationDuration;

	[Header("Players")]
	public PlayerInfo playerInfo;
	public PlayerInfo enemyInfo;

	[HideInInspector] public List<Entity> entities = new List<Entity>();

	bool placedDPS, placedTank, placedSupport;
	[HideInInspector] public bool playerFirst;

	int turn = 0;
	[HideInInspector] public int turnIndex = -1;

	#region Events & Actions

	public Action CoinFlip; //true for player first, false for enemy first

	public delegate void SpellInputHandler(int _spellIndex);
	public static event SpellInputHandler SelectSpell;

	public Action EnterTurn;

	public Action<Archetype> PickArchetype;

	#endregion

	public void PlayNextTurn()
	{
		if(turnIndex > -1)
			entities[turnIndex].isPlaying = false;

		turn++;
		turnIndex++;
		
		if (turnIndex >= entities.Count)
		{
			turnIndex = 0;
		}

		entities[turnIndex].isPlaying = true;
		EnterTurn?.Invoke();

		if (entities[turnIndex].isPlayerEntity)
		{
			SetState("PlayerTurnState", true);
		}
		else
		{
			SetState("AITurnState", true);
		}
	}

	public void RemoveEntity(Entity _entity)
	{
		for (int i = 0; i < entities.Count; i++)
		{
			if (entities[i] == _entity)
			{
				entities.RemoveAt(i);
				return;
			}
		}
	}

	public Action<bool> CoinTossed;

	public IEnumerator CoinFlipCoroutine()
	{
		int _random = UnityEngine.Random.Range(0, 2);
		
		if (_random == 0)
			playerFirst = true;
		else
			playerFirst = false;

		//play coinflip animation

		yield return new WaitForSeconds(coinFlipAnimationDuration); // Animation time

		CoinTossed?.Invoke(playerFirst);
	}

	public void StartPlacingPhase()
	{
		CoinFlip?.Invoke();
	}

	public void EndTurnButton()
	{
		if(ActiveState.StateName == "PlayerTurnState")
		{
			LevelGrid.Instance.HideAllInteractors();
			PlayNextTurn();
		}
	}

	public void InputSpell1()
	{
		if(ActiveState.StateName == "PlayerTurnState")
			SelectSpell?.Invoke(0);
	}

	public void InputSpell2()
	{
		if (ActiveState.StateName == "PlayerTurnState")
			SelectSpell?.Invoke(1);
	}

	public void InputSpell3()
	{
		if (ActiveState.StateName == "PlayerTurnState")
			SelectSpell?.Invoke(2);
	}

	public void EnableArchetypeButtons()
	{
		if (!placedDPS) archetypeButtons[0].SetActive(true);

		if (!placedTank) archetypeButtons[1].SetActive(true);

		if (!placedSupport) archetypeButtons[2].SetActive(true);
	}

	public void DisableArchetypeButtons()
	{
		foreach (GameObject _go in archetypeButtons)
		{
			_go.SetActive(false);
		}
	}

	public void InputDPS()
	{
		placedDPS = true;
		PickArchetype?.Invoke(Archetype.Dps);
	}

	public void InputTank()
	{
		placedTank = true;
		PickArchetype?.Invoke(Archetype.Tank);
	}

	public void InputSupport()
	{
		placedSupport = true;
		PickArchetype?.Invoke(Archetype.Support);
	}

	private void OnDestroy()
	{
		Entity.CatDeath -= RemoveEntity;
	}

	public IEnumerator AnimateParticle(GameObject _particle, Vector2Int _position, bool _move, Vector2Int _targetPos)
	{
		yield return null;
	}
}
