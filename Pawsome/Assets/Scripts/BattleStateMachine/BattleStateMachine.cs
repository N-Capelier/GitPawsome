using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
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

	[Header("Params")]
	[HideInInspector] public InstaCat[] playerCats;
	public InstaCat[] AICats;
	[SerializeField] float coinFlipAnimationDuration;

	[Header("Players")]
	public PlayerInfo playerInfo;
	public PlayerInfo enemyInfo;

	[HideInInspector] public List<Entity> entities = new List<Entity>();

	int turn = 0;
	[HideInInspector] public int turnIndex = -1;

	#region Events & Actions

	public Action<bool> CoinFlip; //true for player first, false for enemy first

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

	public IEnumerator CoinFlipCoroutine()
	{
		int _random = UnityEngine.Random.Range(0, 2);
		
		bool _result;

		if (_random == 0)
			_result = true;
		else
			_result = false;

		yield return new WaitForSeconds(coinFlipAnimationDuration); // Animation time

		CoinFlip?.Invoke(_result);
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

	public void InputDPS()
	{
		PickArchetype?.Invoke(Archetype.Dps);
	}

	public void InputTank()
	{
		PickArchetype?.Invoke(Archetype.Tank);
	}

	public void InputSupport()
	{
		PickArchetype?.Invoke(Archetype.Support);
	}
}
