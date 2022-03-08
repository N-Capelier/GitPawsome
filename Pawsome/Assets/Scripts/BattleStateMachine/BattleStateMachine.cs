using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class BattleStateMachine : MonoStateMachine
{
	[Header("References")]
	public GameObject playerPrefab;
	public GameObject enemyPrefab;
	public BattleUIMode battleUIMode;

	[Header("Params")]
	[HideInInspector] public InstaCat[] playerCats;
	public InstaCat[] AICats;

	/*[HideInInspector]*/ public List<Entity> entities = new List<Entity>();

	int turn = 0;
	/*[HideInInspector]*/ public int turnIndex = -1;

	public delegate void SpellInputHandler(int _spellIndex);
	public static event SpellInputHandler SelectSpell;

	public Action EnterTurn;

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

}
